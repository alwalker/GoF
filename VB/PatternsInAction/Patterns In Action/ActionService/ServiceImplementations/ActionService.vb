Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Xml.Linq

Imports System.Web.Security
Imports System.Collections.Generic

Imports System.ServiceModel
Imports System.ServiceModel.Activation

Imports Cart
Imports Transactions
Imports Encryption
Imports BusinessObjects
Imports DataObjects

Imports ActionService.Criteria
Imports ActionService.DataTransferObjects
Imports ActionService.Messages
Imports ActionService.MessageBase
Imports ActionService.DataTransferObjectMapper
Imports ActionService.ServiceContracts

Namespace ActionService.ServiceImplementations
	''' <summary>
	''' Main facade into Patterns in Action application
	''' </summary>
	''' <remarks>
	''' The Cloud Facade Pattern.
	''' </remarks>
	<ServiceBehavior(InstanceContextMode := InstanceContextMode.PerSession)> _
	Public Class ActionService
		Implements IActionService
		Private Shared methodName As String = ConfigurationManager.AppSettings.Get("ShippingMethod")
		Private Shared defaultShippingMethod As ShippingMethod = CType(System.Enum.Parse(GetType(ShippingMethod), methodName), ShippingMethod)

		' Create static data access objects
		Private Shared productDao As IProductDao = DataAccess.ProductDao
		Private Shared customerDao As ICustomerDao = DataAccess.CustomerDao
		Private Shared orderDao As IOrderDao = DataAccess.OrderDao

		Private _accessToken As String
		Private _shoppingCart As ShoppingCart
		Private _userName As String

		''' <summary>
		''' Gets unique session based token that is valid for the duration of the session.
		''' </summary>
		''' <param name="request">Token request message.</param>
		''' <returns>Token response message.</returns>
		Public Function GetToken(ByVal request As TokenRequest) As TokenResponse Implements IActionService.GetToken
			Dim response As New TokenResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag only
			If (Not ValidRequest(request, response, Validate.ClientTag)) Then
				Return response
			End If

			' Note: these are session based and expire when session expires.
			_accessToken = Guid.NewGuid().ToString()
			_shoppingCart = New ShoppingCart(defaultShippingMethod)

			response.AccessToken = _accessToken
			Return response
		End Function

		''' <summary>
		''' Login to application service.
		''' </summary>
		''' <param name="request">Login request message.</param>
		''' <returns>Login response message.</returns>
		Public Function Login(ByVal request As LoginRequest) As LoginResponse Implements IActionService.Login
			Dim response As New LoginResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag and access token
			If (Not ValidRequest(request, response, Validate.ClientTag Or Validate.AccessToken)) Then
				Return response
			End If

			If (Not Membership.ValidateUser(request.UserName, request.Password)) Then
				response.Acknowledge = AcknowledgeType.Failure
				response.Message = "Invalid username and/or password."
				Return response
			End If

			_userName = request.UserName

			Return response
		End Function

		''' <summary>
		''' Logout from application service.
		''' </summary>
		''' <param name="request">Logout request message.</param>
		''' <returns>Login request message.</returns>
		Public Function Logout(ByVal request As LogoutRequest) As LogoutResponse Implements IActionService.Logout
			Dim response As New LogoutResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag and access token
			If (Not ValidRequest(request, response, Validate.ClientTag Or Validate.AccessToken)) Then
				Return response
			End If

			_userName = Nothing

			Return response
		End Function

		''' <summary>
		''' Request customer data.
		''' </summary>
		''' <param name="request">Customer request message.</param>
		''' <returns>Customer response message.</returns>
		Public Function GetCustomers(ByVal request As CustomerRequest) As CustomerResponse Implements IActionService.GetCustomers
			Dim response As New CustomerResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag, access token, and user credentials
			If (Not ValidRequest(request, response, Validate.All)) Then
				Return response
			End If

			Dim criteria As CustomerCriteria = TryCast(request.Criteria, CustomerCriteria)
			Dim sort As String = criteria.SortExpression

			If request.LoadOptions.Contains("Customers") Then
				Dim customers As IEnumerable(Of Customer)
				If (Not criteria.IncludeOrderStatistics) Then
					' Simple customer list without order information
					customers = customerDao.GetCustomers(criteria.SortExpression)
				ElseIf sort.IndexOf("NumOrders") >= 0 OrElse sort.IndexOf("LastOrderDate") >= 0 Then
					' Sort order is handled by the Order Dao
					Dim list As IList(Of Customer) = customerDao.GetCustomers()
					customers = orderDao.GetOrderStatistics(list, sort)
				Else
					' Sort order is handled by the Customer Dao, but alse need order statistics
					Dim list As IList(Of Customer) = customerDao.GetCustomers(sort)
					customers = orderDao.GetOrderStatistics(list)
				End If
				response.Customers = customers.Select(Function(c) Mapper.ToDataTransferObject(c)).ToList()
			End If

			If request.LoadOptions.Contains("Customer") Then
				Dim customer As Customer = customerDao.GetCustomer(criteria.CustomerId)
				If request.LoadOptions.Contains("Orders") Then
					customer.Orders = orderDao.GetOrders(customer.CustomerId)
				End If

				response.Customer = Mapper.ToDataTransferObject(customer)
			End If

			Return response
		End Function

		''' <summary>
		''' Set (add, update, delete) customer value.
		''' </summary>
		''' <param name="request">Customer request message.</param>
		''' <returns>Customer response message.</returns>
		Public Function SetCustomers(ByVal request As CustomerRequest) As CustomerResponse Implements IActionService.SetCustomers
			Dim response As New CustomerResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag, access token, and user credentials
			If (Not ValidRequest(request, response, Validate.All)) Then
				Return response
			End If

			' Transform customer data transfer object to customer business object
			Dim customer As Customer = Mapper.FromDataTransferObject(request.Customer)

			' Validate customer business rules

			If request.Action <> "Delete" Then
				If (Not customer.Validate()) Then
					response.Acknowledge = AcknowledgeType.Failure

					For Each [error] As String In customer.ValidationErrors
						response.Message &= [error] & Environment.NewLine
					Next [error]

					Return response
				End If
			End If

			' Run within the context of a database transaction. Currently commented out.
			' The Decorator Design Pattern. 
			'using (TransactionDecorator transaction = new TransactionDecorator())
				If request.Action = "Create" Then
					customerDao.InsertCustomer(customer)
					response.Customer = Mapper.ToDataTransferObject(customer)
					response.RowsAffected = 1
				ElseIf request.Action = "Update" Then
					response.RowsAffected = customerDao.UpdateCustomer(customer)
					response.Customer = Mapper.ToDataTransferObject(customer)
				ElseIf request.Action = "Delete" Then
					Dim criteria As CustomerCriteria = TryCast(request.Criteria, CustomerCriteria)
					Dim cust As Customer = customerDao.GetCustomer(criteria.CustomerId)

					response.RowsAffected = customerDao.DeleteCustomer(cust)
				End If

				'transaction.Complete();

			Return response
		End Function

		''' <summary>
		''' Request order data.
		''' </summary>
		''' <param name="request">Order request message.</param>
		''' <returns>Order response message.</returns>
		Public Function GetOrders(ByVal request As OrderRequest) As OrderResponse Implements IActionService.GetOrders
			Dim response As New OrderResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag, access token, and user credentials
			If (Not ValidRequest(request, response, Validate.All)) Then
				Return response
			End If


			Dim criteria As OrderCriteria = TryCast(request.Criteria, OrderCriteria)

			If request.LoadOptions.Contains("Order") Then
				Dim order As Order = orderDao.GetOrder(criteria.OrderId)

				If request.LoadOptions.Contains("Customer") Then
					order.Customer = customerDao.GetCustomerByOrder(order.OrderId)
				End If

				If request.LoadOptions.Contains("OrderDetails") Then
					order.OrderDetails = orderDao.GetOrderDetails(order.OrderId)
				End If

				response.Order = Mapper.ToDataTransferObject(order)
			End If

			If request.LoadOptions.Contains("Orders") Then
				Dim customer As Customer = customerDao.GetCustomer(criteria.CustomerId)

				Dim orders As IList(Of Order) = orderDao.GetOrders(customer.CustomerId)
				If request.LoadOptions.Contains("OrderDetails") Then
					For Each order As Order In orders
						order.OrderDetails = orderDao.GetOrderDetails(order.OrderId)
					Next order
				End If

				response.Orders = Mapper.ToDataTransferObjects(orders)
			End If

			Return response
		End Function

		' Not implemented. No orders are taken.
		Public Function SetOrders(ByVal request As OrderRequest) As OrderResponse Implements IActionService.SetOrders
			Return New OrderResponse()
		End Function

		''' <summary>
		''' Requests product data.
		''' </summary>
		''' <param name="request">Product request message.</param>
		''' <returns>Product response message.</returns>
		Public Function GetProducts(ByVal request As ProductRequest) As ProductResponse Implements IActionService.GetProducts
			Dim response As New ProductResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag and access token
			If (Not ValidRequest(request, response, Validate.ClientTag Or Validate.AccessToken)) Then
				Return response
			End If


			Dim criteria As ProductCriteria = TryCast(request.Criteria, ProductCriteria)

			If request.LoadOptions.Contains("Categories") Then
				Dim categories As IEnumerable(Of Category) = productDao.GetCategories()
				response.Categories = Mapper.ToDataTransferObjects(categories)
			End If

			If request.LoadOptions.Contains("Products") Then
				Dim products As IEnumerable(Of Product) = productDao.GetProductsByCategory(criteria.CategoryId, criteria.SortExpression)
				response.Products = Mapper.ToDataTransferObjects(products)
			End If

			If request.LoadOptions.Contains("Product") Then
				Dim product As Product = productDao.GetProduct(criteria.ProductId)
				product.Category = productDao.GetCategoryByProduct(criteria.ProductId)

				response.Product = Mapper.ToDataTransferObject(product)
			End If

			If request.LoadOptions.Contains("Search") Then
				Dim products As IList(Of Product) = productDao.SearchProducts(criteria.ProductName, criteria.PriceFrom, criteria.PriceThru, criteria.SortExpression)

				response.Products = Mapper.ToDataTransferObjects(products)
			End If
			Return response
		End Function

		' Not implemented. No products are modified.
		Public Function SetProducts(ByVal request As ProductRequest) As ProductResponse Implements IActionService.SetProducts
			Return New ProductResponse()
		End Function

		''' <summary>
		''' Request shopping cart.
		''' </summary>
		''' <param name="request">Shopping cart request message.</param>
		''' <returns>Shopping cart response message.</returns>
		Public Function GetCart(ByVal request As CartRequest) As CartResponse Implements IActionService.GetCart
			Dim response As New CartResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag and access token
			If (Not ValidRequest(request, response, Validate.ClientTag Or Validate.AccessToken)) Then
				Return response
			End If

			' Always return recomputed shopping cart
			response.Cart = Mapper.ToDataTransferObject(_shoppingCart)

			Return response
		End Function

		''' <summary>
		''' Sets (add, edit, delete) shopping cart data.
		''' </summary>
		''' <param name="request">Shopping cart request message.</param>
		''' <returns>Shopping cart response message.</returns>
		Public Function SetCart(ByVal request As CartRequest) As CartResponse Implements IActionService.SetCart
			Dim response As New CartResponse()
			response.CorrelationId = request.RequestId

			' Validate client tag and access token
			If (Not ValidRequest(request, response, Validate.ClientTag Or Validate.AccessToken)) Then
				Return response
			End If

			If request.Action = "Read" Then
				' Do nothing, just return cart    
			ElseIf request.Action = "Create" Then
				_shoppingCart.AddItem(request.CartItem.Id, request.CartItem.Name, request.CartItem.Quantity, request.CartItem.UnitPrice)
			ElseIf request.Action = "Update" Then
				' Either shipping method or quantity requires update
				If (Not String.IsNullOrEmpty(request.ShippingMethod)) Then
					_shoppingCart.ShippingMethod = CType(System.Enum.Parse(GetType(ShippingMethod), request.ShippingMethod), ShippingMethod)
				Else
					_shoppingCart.UpdateQuantity(request.CartItem.Id, request.CartItem.Quantity)
				End If
			ElseIf request.Action = "Delete" Then
				_shoppingCart.RemoveItem(request.CartItem.Id)
			End If

			_shoppingCart.ReCalculate()
			response.Cart = Mapper.ToDataTransferObject(_shoppingCart)

			Return response
		End Function

		''' <summary>
		''' Validate 3 security levels for a request: ClientTag, AccessToken, and User Credentials
		''' </summary>
		''' <param name="request">The request message.</param>
		''' <param name="response">The response message.</param>
		''' <param name="validate">The validation that needs to take place.</param>
		''' <returns></returns>
		Private Function ValidRequest(ByVal request As RequestBase, ByVal response As ResponseBase, ByVal validate As Validate) As Boolean
			' Validate Client Tag. In production this should query a 'client' table in a database.
			If (Validate.ClientTag And validate) = Validate.ClientTag Then
				If request.ClientTag <> "ABC123" Then
					response.Acknowledge = AcknowledgeType.Failure
					response.Message = "Unknown Client Tag"
					Return False
				End If
			End If

			' Validate access token
			If (Validate.AccessToken And validate) = Validate.AccessToken Then
				If _accessToken Is Nothing Then
					response.Acknowledge = AcknowledgeType.Failure
					response.Message = "Invalid or expired AccessToken. Call GetToken()"
					Return False
				End If
			End If

			' Validate user credentials
			If (Validate.UserCredentials And validate) = Validate.UserCredentials Then
				If _userName Is Nothing Then
					response.Acknowledge = AcknowledgeType.Failure
					response.Message = "Please login and provide user credentials before accessing these methods."
					Return False
				End If
			End If


			Return True
		End Function

		#Region "Used in primary key encryption. Not currently used."

		''' <summary>
		''' Encrypts internal identifier before sending it out to client.
		''' Private helper method.
		''' </summary>
		''' <param name="id">Identifier to be encrypted.</param>
		''' <param name="tableName">Database table in which identifier resides.</param>
		''' <returns>Encrypted stringified identifier.</returns>
		Private Function EncryptId(ByVal id As Integer, ByVal tableName As String) As String
			Dim s As String = id.ToString() & "|" & tableName
			Return Crypto.ActionEncrypt(s)
		End Function

		''' <summary>
		''' Decrypts identifiers that come back from client.
		''' Private helper method.
		''' </summary>
		''' <param name="sid">Stringified, encrypted identifier.</param>
		''' <returns>Internal identifier.</returns>
		Private Function DecryptId(ByVal sid As String) As Integer
			Dim s As String = Crypto.ActionDecrypt(sid)
			s = s.Substring(0, s.IndexOf("|"))
			Return Integer.Parse(s)
		End Function

		#End Region

		''' <summary>
		''' Validation options enum. Used in validation of messages.
		''' </summary>
		<Flags> _
		Private Enum Validate
			ClientTag = &H0001
			AccessToken = &H0002
			UserCredentials = &H0004
			All = ClientTag Or AccessToken Or UserCredentials
		End Enum
	End Class
End Namespace
