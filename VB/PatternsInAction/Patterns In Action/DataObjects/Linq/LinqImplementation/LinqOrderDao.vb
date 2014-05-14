Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports DataObjects.Linq.EntityMapper
Imports BusinessObjects
Imports DataObjects.Linq

Namespace DataObjects.Linq.LinqImplementation
	''' <summary>
	''' Linq-to-Sql implementation of the IOrderDao interface.
	''' </summary>
	Public Class LinqOrderDao
		Implements IOrderDao
		#Region "IOrderDao Members"

		''' <summary>
		''' Provides orderstatistic to a given list of customers.
		''' </summary>
		''' <param name="customers">List of customers for which statistics are requested.</param>
		''' <returns>List of customers including their order statistics.</returns>
		Public Function GetOrderStatistics(ByVal customers As IList(Of Customer)) As IList(Of Customer) Implements IOrderDao.GetOrderStatistics
			Return GetOrderStatistics(customers, "")
		End Function

		''' <summary>
		''' Gets a list of customers with order summary statistics.
		''' </summary>
		''' <param name="customers"></param>
		''' <param name="sortExpression"></param>
		''' <returns></returns>
		Public Function GetOrderStatistics(ByVal customers As IList(Of Customer), ByVal sortExpression As String) As IList(Of Customer) Implements IOrderDao.GetOrderStatistics
			' Place customerIds in an integer list
            Dim customerIds As IList(Of Integer) = New List(Of Integer)()
			For Each customer As Customer In customers
				customerIds.Add(customer.CustomerId)
			Next customer

			Using db As ActionDataContext = DataContextFactory.CreateContext()
                Dim query = From o In db.OrderEntities _
                            Where customerIds.Contains(o.CustomerId) _
                            Group o By Key = New With {o.CustomerId} Into g = Group _
                            Select New With {.CustomerId = Key.CustomerId, .LastOrderDate = g.Max(Function(d) d.OrderDate), .NumOrders = g.Count()}

                Dim list As IList(Of Customer) = New List(Of Customer)()

                ' Loop over customer list first to preserve sort order
                For Each customer As Customer In customers
                    For Each item In query
                        If item.CustomerId = customer.CustomerId Then
                            customer.NumOrders = item.NumOrders
                            customer.LastOrderDate = item.LastOrderDate

                            list.Add(customer)
                            Exit For
                        End If
                    Next item
                Next customer

                ' Here we perform in-memory postprocessing of sort order 
                If sortExpression.Length > 0 Then
                    Dim sort() As String = sortExpression.Split(" "c)
                    Dim sortColumn As String = sort(0)
                    Dim sortOrder As String = sort(1)

                    Select Case sortColumn
                        Case "NumOrders"
                            If sortOrder = "ASC" Then
                                list = list.OrderBy(Function(c) c.NumOrders).ToList()
                            Else
                                list = list.OrderByDescending(Function(c) c.NumOrders).ToList()
                            End If
                        Case "LastOrderDate"
                            If sortOrder = "ASC" Then
                                list = list.OrderBy(Function(c) c.LastOrderDate).ToList()
                            Else
                                list = list.OrderByDescending(Function(c) c.LastOrderDate).ToList()
                            End If
                    End Select
                End If
                Return list
            End Using
		End Function

		''' <summary>
		''' Gets all orders for a given customer.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>List of orders.</returns>
		Public Function GetOrders(ByVal customerId As Integer) As IList(Of Order) Implements IOrderDao.GetOrders
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return db.OrderEntities.Where(Function(o) o.CustomerId = customerId).Select(Function(c) Mapper.ToBusinessObject(c)).ToList()
			End Using
		End Function

		''' <summary>
		''' Gets the orders between a given data range.
		''' </summary>
		''' <param name="dateFrom">Start date.</param>
		''' <param name="dateThru">End date.</param>
		''' <returns></returns>
		Public Function GetOrdersByDate(ByVal dateFrom As DateTime, ByVal dateThru As DateTime) As IList(Of Order) Implements IOrderDao.GetOrdersByDate
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return db.OrderEntities.Where(Function(o) o.OrderDate >= dateFrom AndAlso o.OrderDate <= dateThru).Select(Function(c) Mapper.ToBusinessObject(c)).ToList()
			End Using
		End Function

		''' <summary>
		''' Gets the orderdetails for a given order.
		''' </summary>
		''' <param name="orderId">Unique order identifier.</param>
		''' <returns>List of orderdetails.</returns>
		Public Function GetOrderDetails(ByVal orderId As Integer) As IList(Of OrderDetail) Implements IOrderDao.GetOrderDetails
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return db.OrderDetailEntities.Where(Function(od) od.OrderId = orderId).Select(Function(od) Mapper.ToBusinessObject(od)).ToList()
			End Using
		End Function

		''' <summary>
		''' Gets order given an order identifier.
		''' </summary>
		''' <param name="orderId">Order identifier.</param>
		''' <returns>The order.</returns>
		Public Function GetOrder(ByVal orderId As Integer) As Order Implements IOrderDao.GetOrder
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return Mapper.ToBusinessObject(db.OrderEntities.SingleOrDefault(Function(o) o.OrderId = orderId))
			End Using
		End Function

		#End Region
	End Class
End Namespace
