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

Imports ActionServiceReference
Imports ASPNETWebApplication.Controllers

Namespace ASPNETWebApplication.WebAdmin
	Partial Public Class Customers
		Inherits PageBase
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			If (Not IsPostBack) Then
				' Set the selected menu item in the Master page.
				Master.TheMenuInMasterPage.SelectedItem = "customers"

				' Set default sort settings.
				SortColumn = "CustomerId"
				SortDirection = "ASC"

				Bind()
			End If
		End Sub

		''' <summary>
		''' Sets datasources and bind data. 
		''' </summary>
		Private Sub Bind()
			Dim controller As New CustomerController()
			GridViewCustomers.DataSource = controller.GetCustomers(SortExpression)
			GridViewCustomers.DataBind()
		End Sub

		#Region "Sorting"

		''' <summary>
		''' Sets sort order and re-binds page.
		''' </summary>
		Protected Sub GridViewCustomers_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
			SortDirection = If((SortDirection = "ASC"), "DESC", "ASC")
			SortColumn = e.SortExpression

			Bind()
		End Sub

		''' <summary>
		''' Adds glyphs to gridview header according to sort order.
		''' </summary>
		Protected Sub GridViewCustomers_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
			If e.Row.RowType = DataControlRowType.Header Then
				AddGlyph(Me.GridViewCustomers, e.Row)
			End If
		End Sub

		Protected Sub GridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
			If e.Row.RowType = DataControlRowType.DataRow Then
				Dim linkButton As LinkButton = TryCast(e.Row.Cells(5).Controls(0), LinkButton)
				' Escape single quotes in Javascript. 
				Dim company As String = DataBinder.Eval(e.Row.DataItem, "Company").ToString().Replace("'", "\'")
				linkButton.Attributes.Add("onclick", "javascript:return " & "confirm('OK to delete """ & company & """?')")
			End If
		End Sub

		#End Region

		''' <summary>
		''' Deletes selected customer from database.
		''' </summary>
		Protected Sub GridViewCustomers_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
			Dim row As GridViewRow = GridViewCustomers.Rows(e.RowIndex)
			Dim customerId As Integer = Integer.Parse(row.Cells(0).Text)

			Dim controller As New CustomerController()
			Dim rowsAffected As Integer = controller.DeleteCustomer(customerId)
			If rowsAffected = 0 Then
				Dim customerName As String = row.Cells(1).Text
				LabelError.Text = "Cannot delete " & customerName & " because they have existing orders!"
			Else
				Bind()
			End If
		End Sub
	End Class
End Namespace
