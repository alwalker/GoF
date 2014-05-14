Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Data

Imports BusinessObjects

Namespace DataObjects.AdoNet.Oracle
	''' <summary>
	''' Oracle specific data access object that handles data access
	''' of customers. The details are stubbed out (in a crude way) but should be 
	''' relatively easy to implement as they are similar to MS Access and 
	''' Sql Server Data access objects.
	'''
	''' Enterprise Design Pattern: Service Stub.
	''' </summary>
	Public Class OracleCustomerDao
		Implements ICustomerDao
		''' <summary>
		''' Gets a list of all customers.
		''' </summary>
		''' <returns>Customer list.</returns>
		Public Function GetCustomers() As IList(Of Customer) Implements ICustomerDao.GetCustomers
			Dim sortExpression As String = Nothing
			Return GetCustomers(sortExpression)
		End Function

		''' <summary>
		''' Gets a sorted list of all customers. Stubbed.
		''' </summary>
		''' <param name="sortExpression">Sort order.</param>
		''' <returns>Sorted list of customers.</returns>
		Public Function GetCustomers(ByVal sortExpression As String) As IList(Of Customer) Implements ICustomerDao.GetCustomers
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets a customer. Stubbed.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>Customer.</returns>
		Public Function GetCustomer(ByVal customerId As Integer) As Customer Implements ICustomerDao.GetCustomer
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Gets customer given an order. Stubbed.
		''' </summary>
		''' <param name="orderId">Unique order identifier.</param>
		''' <returns>Customer.</returns>
		Public Function GetCustomerByOrder(ByVal orderId As Integer) As Customer Implements ICustomerDao.GetCustomerByOrder
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Inserts a new customer. Stubbed.
		''' </summary>
		''' <remarks>
		''' Following insert, customer object will contain the new identifier.
		''' </remarks>
		''' <param name="customer">Customer.</param>
		Public Sub InsertCustomer(ByVal customer As Customer) Implements ICustomerDao.InsertCustomer
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Sub

		''' <summary>
		''' Updates a customer. Stubbed.
		''' </summary>
		''' <param name="customer">Customer.</param>
		''' <returns>Number of customer records updated.</returns>
		Public Function UpdateCustomer(ByVal customer As Customer) As Integer Implements ICustomerDao.UpdateCustomer
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function

		''' <summary>
		''' Deletes a customer. Stubbed.
		''' </summary>
		''' <param name="customer">Customer</param>
		''' <returns>Number of customer records deleted.</returns>
		Public Function DeleteCustomer(ByVal customer As Customer) As Integer Implements ICustomerDao.DeleteCustomer
			Throw New NotImplementedException("Oracle data access not implemented.")
		End Function
	End Class
End Namespace
