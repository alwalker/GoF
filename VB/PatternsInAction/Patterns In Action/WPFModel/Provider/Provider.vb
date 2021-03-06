Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports System.Collections.ObjectModel
Imports System.Configuration

Imports WPFModel.DataTransferObjectMapper
Imports ActionServiceReference
Imports WPFModel.BusinessModelObjects

Imports WCFProxy

Namespace WPFModel.Provider
	''' <summary>
	''' Implementation of provider interface to (data) Services.
	''' </summary>
	Public Class Provider
		Implements IProvider
		#Region "statics"

        Private Shared _service As ActionServiceClient
		Private Shared Property Service() As ActionServiceClient
			Get
                Return _service
			End Get
			Set(ByVal value As ActionServiceClient)
                _service = value
			End Set
		End Property
        Private Shared _accessToken As String
		Private Shared Property AccessToken() As String
			Get
                Return _accessToken
			End Get
			Set(ByVal value As String)
                _accessToken = value
			End Set
		End Property
        Private Shared _clientTag As String
		Private Shared Property ClientTag() As String
			Get
                Return _clientTag
			End Get
			Set(ByVal value As String)
                _clientTag = value
			End Set
		End Property

        Delegate Sub SomeAction(ByRef request As TokenRequest, ByRef response As TokenResponse)

		''' <summary>
		''' Static constructor. 
		''' </summary>
		Shared Sub New()
			Service = New ActionServiceClient()

			' Gets client tag from app.config configuration file
			ClientTag = ConfigurationManager.AppSettings.Get("ClientTag")

			' Retrieve AccessToken as first step
			Dim request As New TokenRequest()
			request.RequestId = NewRequestId
			request.ClientTag = ClientTag

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response = Service.GetToken(request)

            ' Store access token for subsequent service calls.
			AccessToken = response.AccessToken
        End Sub
        


		''' <summary>
		''' Gets a unique request id.
		''' </summary>
		Private Shared ReadOnly Property NewRequestId() As String
			Get
				Return Guid.NewGuid().ToString()
			End Get
		End Property

		#End Region

		#Region "Login / Logout"

		''' <summary>
		''' Logs in to the service.
		''' </summary>
		''' <param name="userName">User name.</param>
		''' <param name="password">Password.</param>
		Public Sub Login(ByVal userName As String, ByVal password As String) Implements IProvider.Login
			Dim request As New LoginRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.UserName = userName
			request.Password = password

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response = Service.Login(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("Login: RequestId and CorrelationId do not match.")
			End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If
		End Sub

		''' <summary>
		''' Logs out of the service.
		''' </summary>
		Public Sub Logout() Implements IProvider.Logout
			Dim request As New LogoutRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response = Service.Logout(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("Logout: RequestId and CorrelationId do not match.")
			End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If
		End Sub

		#End Region

		#Region "Customers "

		''' <summary>
		''' Gets an observable collection of all customers in the given sort order.
		''' </summary>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>List of customers.</returns>
		Public Function GetCustomers(ByVal sortExpression As String) As ObservableCollection(Of CustomerModel) Implements IProvider.GetCustomers
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Customers" }
			request.Criteria = New CustomerCriteria With {.SortExpression = sortExpression}

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response As CustomerResponse = Service.GetCustomers(request)

            If request.RequestId <> response.CorrelationId Then
                Throw New ApplicationException("GetCustomers: RequestId and CorrelationId do not match.")
            End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If

			Return Mapper.FromDataTransferObjects(response.Customers, Me)
		End Function

		''' <summary>
		''' Gets a specific customer.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>Customer.</returns>
		Public Function GetCustomer(ByVal customerId As Integer) As CustomerModel Implements IProvider.GetCustomer
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Customer" }
			request.Criteria = New CustomerCriteria With {.CustomerId = customerId}

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response As CustomerResponse = Service.GetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetCustomer: RequestId and CorrelationId do not match.")
			End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If

			Return Mapper.FromDataTransferObject(response.Customer, Me)
		End Function

		#End Region

		#Region "Customer persistence"

		''' <summary>
		''' Adds a new customer to the database.
		''' </summary>
		''' <param name="customer">Customer.</param>
		''' <returns>Number of records affected. If all worked well, then should be 1.</returns>
		Public Function AddCustomer(ByVal customer As CustomerModel) As Integer Implements IProvider.AddCustomer
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Create"
			request.Customer = Mapper.ToDataTransferObject(customer)

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response As CustomerResponse = Service.SetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("AddCustomer: RequestId and CorrelationId do not match.")
			End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If

			' Update version & new customerId
			customer.Version = response.Customer.Version
			customer.CustomerId = response.Customer.CustomerId

			Return response.RowsAffected
		End Function

		''' <summary>
		''' Updates an existing customer in the database.
		''' </summary>
		''' <param name="customer">The updated customer.</param>
		''' <returns>Number or records affected. Should be 1.</returns>
		Public Function UpdateCustomer(ByVal customer As CustomerModel) As Integer Implements IProvider.UpdateCustomer
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Update"
			request.Customer = Mapper.ToDataTransferObject(customer)

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response As CustomerResponse = Service.SetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("UpdateCustomer: RequestId and CorrelationId do not match.")
			End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If

			' Update version
			customer.Version = response.Customer.Version

			Return response.RowsAffected
		End Function

		''' <summary>
		''' Deletes a customer record.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>Number of records affected. Should be 1.</returns>
		Public Function DeleteCustomer(ByVal customerId As Integer) As Integer Implements IProvider.DeleteCustomer
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Delete"
			request.Criteria = New CustomerCriteria With {.CustomerId = customerId}

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response As CustomerResponse = Service.SetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("DeleteCustomer: RequestId and CorrelationId do not match.")
			End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If

			Return response.RowsAffected
		End Function

		#End Region

		#Region "Orders"

		''' <summary>
		''' Gets an observable collection of orders for a given customer.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>List of orders.</returns>
		Public Function GetOrders(ByVal customerId As Integer) As ObservableCollection(Of OrderModel) Implements IProvider.GetOrders
			Dim request As New OrderRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Orders", "OrderDetails" }
			request.Criteria = New OrderCriteria With {.CustomerId = customerId, .SortExpression = "OrderId ASC"}

            ' Did not use SaveProxy (as in C#) because VB doesn't support anonymous methods.
            Dim response As OrderResponse = Service.GetOrders(request)

            If request.RequestId <> response.CorrelationId Then
                Throw New ApplicationException("GetOrders: RequestId and CorrelationId do not match.")
            End If

			If response.Acknowledge <> AcknowledgeType.Success Then
				Throw New ApplicationException(response.Message)
			End If

			Return Mapper.FromDataTransferObjects(response.Orders)
		End Function

		#End Region
	End Class
End Namespace
