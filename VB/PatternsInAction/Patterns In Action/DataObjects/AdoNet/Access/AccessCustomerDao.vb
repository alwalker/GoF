Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports BusinessObjects

Namespace DataObjects.AdoNet.Access
	''' <summary>
	''' Microsoft Access specific data access object that handles data access
	''' of customers.
	''' </summary>
	Public Class AccessCustomerDao
		Implements ICustomerDao
		''' <summary>
		''' Gets a list of all customers.
		''' </summary>
		''' <returns>Customer list.</returns>
		Public Function GetCustomers() As IList(Of Customer) Implements ICustomerDao.GetCustomers
			' Set default sortExpression
			Return GetCustomers("CustomerId ASC")
		End Function

		''' <summary>
		''' Gets a sorted list of all customers.
		''' </summary>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Sorted list of customers.</returns>
		Public Function GetCustomers(ByVal sortExpression As String) As IList(Of Customer) Implements ICustomerDao.GetCustomers
			Dim sql As New StringBuilder()
			sql.Append(" SELECT CustomerId, CompanyName, City, Country ")
			sql.Append("   FROM Customer ")
			If (Not String.IsNullOrEmpty(sortExpression)) Then
				sql.Append(" ORDER BY " & sortExpression)
			End If

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())

			Return MakeCustomers(dt)
		End Function

		Private Function MakeCustomers(ByVal dt As DataTable) As IList(Of Customer)
			Dim list As IList(Of Customer) = New List(Of Customer)()
			For Each row As DataRow In dt.Rows
				list.Add(MakeCustomer(row))
			Next row

			Return list
		End Function

		Private Function MakeCustomer(ByVal row As DataRow) As Customer
			Dim customerId As Integer = Integer.Parse(row("CustomerId").ToString())
			Dim company As String = row("CompanyName").ToString()
			Dim city As String = row("City").ToString()
			Dim country As String = row("Country").ToString()

			Return New Customer(customerId, company, city, country)
		End Function

		''' <summary>
		''' Gets a customer.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>Customer.</returns>
		Public Function GetCustomer(ByVal customerId As Integer) As Customer Implements ICustomerDao.GetCustomer
			Dim sql As New StringBuilder()
			sql.Append(" SELECT CustomerId, CompanyName, City, Country ")
			sql.Append("   FROM Customer")
			sql.Append("  WHERE CustomerId = " & customerId)

			Dim row As DataRow = Db.GetDataRow(sql.ToString())
			If row Is Nothing Then
				Return Nothing
			End If

			Return MakeCustomer(row)
		End Function

		''' <summary>
		''' Gets customer given an order.
		''' </summary>
		''' <param name="orderId">Unique order identifier.</param>
		''' <returns>Customer.</returns>
		Public Function GetCustomerByOrder(ByVal orderId As Integer) As Customer Implements ICustomerDao.GetCustomerByOrder
			Dim sql As New StringBuilder()
			sql.Append(" SELECT Customer.CustomerId, CompanyName, City, Country ")
			sql.Append("   FROM [Order] LEFT JOIN Customer ON [Order].CustomerId = Customer.CustomerId ")
			sql.Append("  WHERE OrderId = " & orderId)

			Dim row As DataRow = Db.GetDataRow(sql.ToString())
			If row Is Nothing Then
				Return Nothing
			End If

			Return MakeCustomer(row)

			'Customer customer = MakeCustomer(row);
			'order.Customer = customer;
			'return customer;
		End Function

		''' <summary>
		''' Inserts a new customer. 
		''' </summary>
		''' <remarks>
		''' Following insert, customer object will contain the new identifier.
		''' </remarks>
		''' <param name="customer">Customer.</param>
		Public Sub InsertCustomer(ByVal customer As Customer) Implements ICustomerDao.InsertCustomer
			Dim sql As New StringBuilder()
			sql.Append(" INSERT INTO Customer (CompanyName, City, Country) ")
			sql.Append("  VALUES( " & Db.Escape(customer.Company) & ", ")
			sql.Append("          " & Db.Escape(customer.City) & ", ")
			sql.Append("          " & Db.Escape(customer.Country) & ") ")

			' Assign new customer Id back to business object
			Dim newId As Integer = Db.Insert(sql.ToString(),True)
			customer.CustomerId = newId
		End Sub

		''' <summary>
		''' Updates a customer.
		''' </summary>
		''' <param name="customer">Customer.</param>
		''' <returns>Number of customer records updated.</returns>
		Public Function UpdateCustomer(ByVal customer As Customer) As Integer Implements ICustomerDao.UpdateCustomer
			Dim sql As New StringBuilder()
			sql.Append(" UPDATE Customer ")
			sql.Append("    SET CompanyName = " & Db.Escape(customer.Company) & ", ")
			sql.Append("        City = " & Db.Escape(customer.City) & ", ")
			sql.Append("        Country = " & Db.Escape(customer.Country) & " ")
			sql.Append("  WHERE CustomerId = " & customer.CustomerId)

			Return Db.Update(sql.ToString())
		End Function

		''' <summary>
		''' Deletes a customer.
		''' </summary>
		''' <param name="customer">Customer.</param>
		''' <returns>Number of customer records deleted.</returns>
		Public Function DeleteCustomer(ByVal customer As Customer) As Integer Implements ICustomerDao.DeleteCustomer
			Dim sql As New StringBuilder()
			sql.Append(" DELETE FROM Customer  ")
			sql.Append("  WHERE CustomerId = " & customer.CustomerId)

			Try
				Return Db.Update(sql.ToString())
			Catch
				Return 0
			End Try
		End Function
	End Class
End Namespace
