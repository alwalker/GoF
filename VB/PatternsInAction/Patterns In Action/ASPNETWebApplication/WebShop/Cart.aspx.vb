Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Xml.Linq

Imports ASPNETWebApplication.Controllers
Imports ActionServiceReference

Namespace ASPNETWebApplication.WebShop
	Partial Public Class Cart
		Inherits PageBase
		Private _controller As CartController
		Private _shoppingCart As ShoppingCart

		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			' Initialize controller and cart
			_controller = New CartController()
			_shoppingCart = _controller.GetCart()

			If (Not IsPostBack) Then
				' Set the selected menu item in Master page.
				Master.TheMenuInMasterPage.SelectedItem = "cart"

				Bind()
			End If
		End Sub

		''' <summary>
		''' Sets shopping cart datasource and databind to controls.
		''' </summary>
		Private Sub Bind()
			' Disable controls when cart is empty.
			If _shoppingCart.CartItems.Length = 0 Then
				HyperLinkCheckout.Enabled = False
				HyperLinkCheckoutBottom.Enabled = False
				LinkbuttonRecalculate.Enabled = False
			End If

			' Set the selected shipping method in dropdown control.
			If _shoppingCart.ShippingMethod = "Fedex" Then
				DropDownListShipping.SelectedValue = "1"
			ElseIf _shoppingCart.ShippingMethod = "UPS" Then
				DropDownListShipping.SelectedValue = "2"
			Else
				DropDownListShipping.SelectedValue = "3"
			End If

			' Databind cart to gridview control
			GridViewCart.DataSource = _shoppingCart.CartItems
			Page.DataBind()
		End Sub

		''' <summary>
		''' Gets the total cost of items in shopping cart.
		''' </summary>
		''' <returns>Total cost.</returns>
		Protected Function Total() As Double
			Return _shoppingCart.Total
		End Function

		''' <summary>
		''' Gets the subtotal cost of items in shopping cart.
		''' </summary>
		''' <returns>Subtotal cost.</returns>
		Protected Function SubTotal() As Double
			Return _shoppingCart.SubTotal
		End Function

		''' <summary>
		''' Gets the shipping cost (freight) for items in shopping cart.
		''' </summary>
		''' <returns>Shipping cost.</returns>
		Protected Function Shipping() As Double
			Return _shoppingCart.Shipping
		End Function

		''' <summary>
		''' Adjust shopping cart quantities and then recalculates totals and subtotals.
		''' </summary>
		Protected Sub LinkbuttonRecalculate_Click(ByVal sender As Object, ByVal e As System.EventArgs)
			' Check if there are any items in the cart.
			If _shoppingCart.CartItems.Length = 0 Then
				Return
			End If

			For Each row As GridViewRow In GridViewCart.Rows
				Dim textBox As TextBox = CType(row.Cells(0).FindControl("TextBoxQuantity"), TextBox)

				Dim quantity As Integer
				If Integer.TryParse(textBox.Text, quantity) Then
					Dim productId As Integer = Integer.Parse((CType(row.Cells(0).FindControl("TextBoxId"), TextBox)).Text)

					If quantity <= 0 Then
						_shoppingCart = _controller.RemoveItem(productId)
					ElseIf quantity > 0 AndAlso quantity < 100 Then
						_shoppingCart = _controller.UpdateQuantity(productId, quantity)
					End If
				End If
			Next row
			Bind()
		End Sub

		''' <summary>
		''' Changes to selected shipping method and rebinds new settings to page.
		''' </summary>
		Protected Sub DropDownListShipping_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
			' Change to selected shipping method (i.e. strategy pattern).
			Dim method As String = Me.DropDownListShipping.SelectedItem.Text

			_shoppingCart = _controller.SetShippingMethod(method)

			Bind()
		End Sub

		''' <summary>
		''' Removes an item from the shopping cart.
		''' </summary>
		Protected Sub GridViewCart_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
			Dim viewRow As GridViewRow = GridViewCart.Rows(e.RowIndex)
			Dim textBoxId As TextBox = CType(viewRow.Cells(0).FindControl("TextBoxId"), TextBox)

			Dim productId As Integer
			If Integer.TryParse(textBoxId.Text, productId) Then
				_shoppingCart = _controller.RemoveItem(productId)
				Bind()
			End If
		End Sub
	End Class
End Namespace
