Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports BusinessObjects

Namespace DataObjects.AdoNet.Oracle
	''' <summary>
	''' Oracle specific data access object that handles data access
	''' of customer related orders and order details. The details are stubbed out (in a crude way) but should be 
	''' relatively easy to implement as they are similar to MS Access and 
	''' Sql Server Data access objects.
	'''
	''' Enterprise Design Pattern: Service Stub.
	''' </summary>
	Public Class OracleOrderDao
		Implements IOrderDao
		''' <summary>
		''' Gets customers with order statistics in given sort order. Stubbed.
		''' </summary>
		''' <param name="customers">Customer list.</param>
		''' <returns>Sorted list of customers with order statistics.</returns>
		Public Function GetOrderStatistics(ByVal customers As IList(Of Customer)) As IList(Of Customer) Implements IOrderDao.GetOrderStatistics
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a list of customers with order summary statistics. Stubbed.
		''' </summary>
		''' <param name="customers">Customer list.</param>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Customer list with order summary statistics.</returns>
		Public Function GetOrderStatistics(ByVal customers As IList(Of Customer), ByVal sortExpression As String) As IList(Of Customer) Implements IOrderDao.GetOrderStatistics
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets all orders for a customer. Stubbed.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>List of orders.</returns>
		Public Function GetOrders(ByVal customerId As Integer) As IList(Of Order) Implements IOrderDao.GetOrders
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a list of orders placed within a date range. Stubbed.
		''' </summary>
		''' <param name="dateFrom">Date range begin date.</param>
		''' <param name="dateThru">Date range end date.</param>
		''' <returns>List of orders.</returns>
		Public Function GetOrdersByDate(ByVal dateFrom As DateTime, ByVal dateThru As DateTime) As IList(Of Order) Implements IOrderDao.GetOrdersByDate
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a list of order details for a given order. Stubbed.
		''' </summary>
		''' <param name="orderId">Unique order identifier.</param>
		''' <returns>List of order details.</returns>
		Public Function GetOrderDetails(ByVal orderId As Integer) As IList(Of OrderDetail) Implements IOrderDao.GetOrderDetails
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a list of orders. Private helper method. Stubbed.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>List of orders.</returns>
		Private Function GetOrderList(ByVal sql As String) As IList(Of Order)
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets an order. Stubbed.
		''' </summary>
		''' <param name="orderId">Unique order identifier.</param>
		''' <returns>Order.</returns>
		Public Function GetOrder(ByVal orderId As Integer) As Order Implements IOrderDao.GetOrder
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function
	End Class
End Namespace
