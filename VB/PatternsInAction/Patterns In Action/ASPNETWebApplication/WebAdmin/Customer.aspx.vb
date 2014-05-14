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

Namespace ASPNETWebApplication.WebAdmin
	Partial Public Class Customer
		Inherits PageBase
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			If (Not IsPostBack) Then
				' Set the selected menu item in the Master page.
				Master.TheMenuInMasterPage.SelectedItem = "customers"

				CustomerId = Integer.Parse(Request("id").ToString())

				' Set DetailsView control in Add or Edit mode.
				If CustomerId = 0 Then
					DetailsViewCustomer.ChangeMode(DetailsViewMode.Insert)
				Else
					DetailsViewCustomer.ChangeMode(DetailsViewMode.Edit)
				End If

				' Set image
				ImageCustomer.ImageUrl = imageService & "GetCustomerImageLarge/" & CustomerId
			End If
		End Sub

		''' <summary>
		''' Gets or sets the customerId for this page.
		''' </summary>
		Private Property CustomerId() As Integer
			Get
				Return Integer.Parse(Session("CustomerId").ToString())
			End Get
			Set(ByVal value As Integer)
				Session("CustomerId") = value
			End Set
		End Property

		''' <summary>
		''' Saves data for new or edited customer to database.
		''' </summary>
		Protected Sub ButtonSave_Click(ByVal sender As Object, ByVal e As EventArgs)
			Dim controller As New CustomerController()

			Dim customer As ActionServiceReference.Customer
			If CustomerId = 0 Then
				customer = New ActionServiceReference.Customer()
			Else
				customer = controller.GetCustomer(CustomerId)
			End If

			' Get Company name from page.
			Dim row As DetailsViewRow = DetailsViewCustomer.Rows(1)
			Dim textBox As TextBox = TryCast(row.Cells(1).Controls(0), TextBox)
			customer.Company = textBox.Text.Trim()

			' Get City from page.
			row = DetailsViewCustomer.Rows(2)
			textBox = TryCast(row.Cells(1).Controls(0), TextBox)
			customer.City = textBox.Text.Trim()

			' Get Country from page.
			row = DetailsViewCustomer.Rows(3)
			textBox = TryCast(row.Cells(1).Controls(0), TextBox)
			customer.Country = textBox.Text.Trim()

			Try
				If CustomerId = 0 Then
					controller.AddCustomer(customer)
				Else
					controller.UpdateCustomer(customer)
				End If
			Catch ex As ApplicationException
				LabelError.Text = ex.Message.Replace(Environment.NewLine, "<br />")
				LabelError.Visible = True
				Return
			End Try

			' Return to list of customers.
			Response.Redirect("Customers.aspx")

		End Sub

		''' <summary>
		''' Cancel the page and redirect user to page with list of customers.
		''' </summary>
		Protected Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
			Response.Redirect("Customers.aspx")
		End Sub

		''' <summary>
		''' Executed only once. Used to place cursor in first editable field.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Protected Sub DetailsView_OnDataBound(ByVal sender As Object, ByVal e As EventArgs)
			Dim row As DetailsViewRow = DetailsViewCustomer.Rows(1)
			Dim textBox As TextBox = TryCast(row.Cells(1).Controls(0), TextBox)
			textBox.Focus()
		End Sub
	End Class
End Namespace
