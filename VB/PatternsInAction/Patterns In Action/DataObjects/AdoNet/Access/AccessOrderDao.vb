Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports BusinessObjects

Namespace DataObjects.AdoNet.Access
	''' <summary>
	''' Microsoft Access specific data access object that handles data access
	''' of customer related orders and order details.
	''' </summary>
	Public Class AccessOrderDao
		Implements IOrderDao
		''' <summary>
		''' Gets customers with order statistics in given sort order.
		''' </summary>
		''' <param name="customers">Customer list.</param>
		''' <returns>Sorted list of customers with order statistics.</returns>
		Public Function GetOrderStatistics(ByVal customers As IList(Of Customer)) As IList(Of Customer) Implements IOrderDao.GetOrderStatistics
			Dim customerIds As String = CommaSeparateCustomerIds(customers)

			Dim sql As New StringBuilder()
			sql.Append(" SELECT CustomerId, MAX(OrderDate) AS LastOrderDate, COUNT(OrderId) AS NumOrders ")
			sql.Append("   FROM [Order]")
			sql.Append("  WHERE CustomerId IN (" & customerIds & ")")
			sql.Append("  GROUP BY CustomerId ")

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())

			' Loop over customers first to preserve sort order
			For Each customer As Customer In customers
				For Each row As DataRow In dt.Rows
					If Integer.Parse(row("CustomerId").ToString()) = customer.CustomerId Then
						customer.NumOrders = Integer.Parse(row("NumOrders").ToString())
						customer.LastOrderDate = DateTime.Parse(row("LastOrderDate").ToString())

						Exit For
					End If
				Next row
			Next customer

			Return customers
		End Function

		' Generates string of comma separated ids
		Private Function CommaSeparateCustomerIds(ByVal customers As IList(Of Customer)) As String
			Dim sb As New StringBuilder()
			For Each customer As Customer In customers
				sb.Append(customer.CustomerId)
				sb.Append(",")
			Next customer
			Return sb.Remove(sb.Length - 1, 1).ToString()
		End Function

		''' <summary>
		''' Gets a list of customers with order summary statistics.
		''' </summary>
		''' <param name="customers">Customer list.</param>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Customer list with order summary statistics.</returns>
		Public Function GetOrderStatistics(ByVal customers As IList(Of Customer), ByVal sortExpression As String) As IList(Of Customer) Implements IOrderDao.GetOrderStatistics
			Dim customerIds As String = CommaSeparateCustomerIds(customers)

			Dim sql As New StringBuilder()
			sql.Append(" SELECT CustomerId, MAX(OrderDate) AS LastOrderDate, COUNT(OrderId) AS NumOrders ")
			sql.Append("   FROM [Order]")
			sql.Append("  WHERE CustomerId IN (" & customerIds & ")")
			sql.Append("  GROUP BY CustomerId ")

			If (Not String.IsNullOrEmpty(sortExpression)) Then
				' MS Access does not ORDER BY column alias name
				' Change alias to aggregate function
				sortExpression = sortExpression.Replace("LastOrderDate", "MAX(OrderDate)")
				sortExpression = sortExpression.Replace("NumOrders", "COUNT(OrderId)")

				sql.Append("  ORDER BY " & sortExpression)
			End If

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())

			Dim list As IList(Of Customer) = New List(Of Customer)()

			' Loop over datatable rows first to preserve sort order.
			For Each row As DataRow In dt.Rows
				For Each customer As Customer In customers
					If Integer.Parse(row("CustomerId").ToString()) = customer.CustomerId Then
						customer.NumOrders = Integer.Parse(row("NumOrders").ToString())
						customer.LastOrderDate = DateTime.Parse(row("LastOrderDate").ToString())

						list.Add(customer)
						Exit For
					End If
				Next customer
			Next row

			Return list
		End Function

		''' <summary>
		''' Gets all orders for a customer.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>Order list.</returns>
		Public Function GetOrders(ByVal customerId As Integer) As IList(Of Order) Implements IOrderDao.GetOrders
			Dim sql As New StringBuilder()
			sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ")
			sql.Append("   FROM [Order]")
			sql.Append("  WHERE CustomerId = " & customerId)
			sql.Append("  ORDER BY OrderDate ASC ")

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())
			Return MakeOrders(dt)
		End Function

		''' <summary>
		''' Gets a list of orders placed within a date range.
		''' </summary>
		''' <param name="dateFrom">Date range begin date.</param>
		''' <param name="dateThru">Date range end date.</param>
		''' <returns>List of orders.</returns>
		Public Function GetOrdersByDate(ByVal dateFrom As DateTime, ByVal dateThru As DateTime) As IList(Of Order) Implements IOrderDao.GetOrdersByDate
			Dim sql As New StringBuilder()
			sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ")
			sql.Append("   FROM [Order]")
			sql.Append("  WHERE OrderDate >= '" & dateFrom & "' ")
			sql.Append("    AND OrderDate <= '" & dateThru & "' ")
			sql.Append("  ORDER BY OrderDate ASC ")

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())
			Return MakeOrders(dt)
		End Function

		''' <summary>
		''' Gets a list of order details for a given order.
		''' </summary>
		''' <param name="orderId">Unique order identifier.</param>
		''' <returns>List of order details.</returns>
		Public Function GetOrderDetails(ByVal orderId As Integer) As IList(Of OrderDetail) Implements IOrderDao.GetOrderDetails
			Dim sql As New StringBuilder()
			sql.Append(" SELECT ProductName, O.UnitPrice, Quantity, Discount ")
			sql.Append("   FROM OrderDetail O INNER JOIN Product ON O.ProductId = Product.ProductId ")
			sql.Append("  WHERE O.OrderId = " & orderId)

			Dim dt As DataTable = Db.GetDataTable(sql.ToString())
			Return MakeOrderDetails(dt)
		End Function

		Private Function MakeOrderDetails(ByVal dt As DataTable) As IList(Of OrderDetail)
			Dim list As IList(Of OrderDetail) = New List(Of OrderDetail)()
			For Each row As DataRow In dt.Rows
				list.Add(MakeOrderDetail(row))
			Next row

			Return list
		End Function

		Private Function MakeOrderDetail(ByVal row As DataRow) As OrderDetail
			Dim product As String = row("ProductName").ToString()
			Dim quantity As Integer = Integer.Parse(row("Quantity").ToString())
			Dim unitPrice As Single = Single.Parse(row("UnitPrice").ToString())
			Dim discount As Single = Single.Parse(row("Discount").ToString())
			Dim order As Order = Nothing

			Return New OrderDetail(product, quantity, unitPrice, discount, order)
		End Function

		''' <summary>
		''' Gets a list of orders. Private helper method.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>Order list.</returns>
		Private Function MakeOrders(ByVal dt As DataTable) As IList(Of Order)
			Dim list As IList(Of Order) = New List(Of Order)()
			For Each row As DataRow In dt.Rows
				list.Add(MakeOrder(row))
			Next row

			Return list
		End Function

		Private Function MakeOrder(ByVal row As DataRow) As Order
			Dim orderId As Integer = Integer.Parse(row("OrderId").ToString())
			Dim orderDate As DateTime = DateTime.Parse(row("OrderDate").ToString())
			Dim requiredDate As DateTime = DateTime.Parse(row("RequiredDate").ToString())
			Dim freight As Single = Single.Parse(row("Freight").ToString())

			Return New Order(orderId, orderDate, requiredDate, freight)
		End Function

		''' <summary>
		''' Gets a specific order.
		''' </summary>
		''' <param name="orderId">Unique order identifier.</param>
		''' <returns>Order.</returns>
		Public Function GetOrder(ByVal orderId As Integer) As Order Implements IOrderDao.GetOrder
			Dim sql As New StringBuilder()
			sql.Append(" SELECT OrderId, OrderDate, RequiredDate, Freight ")
			sql.Append("   FROM [Order] ")
			sql.Append("  WHERE OrderId = " & orderId)

			Dim row As DataRow = Db.GetDataRow(sql.ToString())
			If row Is Nothing Then
				Return Nothing
			End If

			Return MakeOrder(row)
		End Function
	End Class
End Namespace
