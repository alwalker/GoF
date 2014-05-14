Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.ComponentModel

Imports ActionServiceReference

Namespace ASPNETWebApplication.Controllers
	''' <summary>
	''' Controller for the shopping cart.
	''' </summary>
	''' <remarks>
	''' MV Patterns: Model View Controller Pattern.
	''' This is a 'loose' implementation of the MVC pattern.
	''' </remarks>
	<DataObject(True)> _
	Public Class CartController
		Inherits ControllerBase
		''' <summary>
		''' Gets the user's cart.
		''' </summary>
		''' <returns>Shopping cart.</returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetCart() As ShoppingCart
			Dim request As New CartRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Read"

			Dim response As CartResponse = ActionServiceClient.GetCart(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetCart: Request and CorrelationId do not match.")
			End If

			Return response.Cart
		End Function

		''' <summary>
		''' Adds an item to the shopping cart.
		''' </summary>
		''' <param name="productId">Unique product identifier or item.</param>
		''' <param name="name">Item name.</param>
		''' <param name="quantity">Quantity of items.</param>
		''' <param name="unitPrice">Unit price for each item.</param>
		''' <returns>Updated shopping cart.</returns>
		Public Function AddItem(ByVal productId As Integer, ByVal name As String, ByVal quantity As Integer, ByVal unitPrice As Double) As ShoppingCart
			Dim request As New CartRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Create"
			request.CartItem = New ShoppingCartItem With {.Id = productId, .Name = name, .Quantity = quantity, .UnitPrice = unitPrice}

			Dim response As CartResponse = ActionServiceClient.SetCart(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("AddItem: Request and CorrelationId do not match.")
			End If

			Return response.Cart
		End Function

		''' <summary>
		''' Removes a line item from the shopping cart.
		''' </summary>
		''' <param name="productId">The item to be removed.</param>
		''' <returns>Updated shopping cart.</returns>
		Public Function RemoveItem(ByVal productId As Integer) As ShoppingCart
			Dim request As New CartRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Delete"
			request.CartItem = New ShoppingCartItem With {.Id = productId}

			Dim response As CartResponse = ActionServiceClient.SetCart(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("RemoveItem: Request and CorrelationId do not match.")
			End If

			Return response.Cart
		End Function

		''' <summary>
		''' Updates a line item in the shopping cart with a new quantity.
		''' </summary>
		''' <param name="productId">Unique product line item.</param>
		''' <param name="quantity">New quantity.</param>
		''' <returns>Updated shopping cart.</returns>
		Public Function UpdateQuantity(ByVal productId As Integer, ByVal quantity As Integer) As ShoppingCart
			Dim request As New CartRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Update"
			request.CartItem = New ShoppingCartItem With {.Id = productId, .Quantity = quantity}

			Dim response As CartResponse = ActionServiceClient.SetCart(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("UpdateItem: Request and CorrelationId do not match.")
			End If

			Return response.Cart
		End Function

		''' <summary>
		''' Sets shipping method used to compute shipping charges.
		''' </summary>
		''' <param name="shippingMethod">The name of the shipper.</param>
		''' <returns>Updated shopping cart.</returns>
		Public Function SetShippingMethod(ByVal shippingMethod As String) As ShoppingCart
			Dim request As New CartRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Update"
			request.ShippingMethod = shippingMethod

			Dim response As CartResponse = ActionServiceClient.SetCart(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("SetShippingMethod: Request and CorrelationId do not match.")
			End If

			Return response.Cart
		End Function
	End Class
End Namespace
