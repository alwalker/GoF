Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Xml.Linq
Imports System.ComponentModel

Imports ActionServiceReference

Namespace ASPNETWebApplication.Controllers
	''' <summary>
	''' Controller for customer related objects.
	''' </summary>
	''' <remarks>
	''' MV Patterns: Model View Controller Pattern.
	''' This is a 'loose' implementation of the MVC pattern.
	''' </remarks>
	Public Class CustomerController
		Inherits ControllerBase
		''' <summary>
		''' Gets a list of customers.
		''' </summary>
		''' <param name="sortExpression">Desired sort order of the customer list.</param>
		''' <returns>List of customers.</returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetCustomers(ByVal sortExpression As String) As IList(Of Customer)
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Customers" }
			request.Criteria = New CustomerCriteria With {.SortExpression = sortExpression}

			Dim response As CustomerResponse = ActionServiceClient.GetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetCustomers: RequestId and CorrelationId do not match.")
			End If

			Return response.Customers
		End Function

		''' <summary>
		''' Gets list of customers, each of which contains order statistics.
		''' </summary>
		''' <param name="sortExpression">Sortorder for returned customer list.</param>
		''' <returns>Sorted customer list with order stats.</returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetCustomersWithOrderStatistics(ByVal sortExpression As String) As IList(Of Customer)
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Customers" }
			request.Criteria = New CustomerCriteria With {.SortExpression = sortExpression, .IncludeOrderStatistics = True}

			Dim response As CustomerResponse = ActionServiceClient.GetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetCustomers: RequestId and CorrelationId do not match.")
			End If

			Return response.Customers
		End Function

		''' <summary>
		''' Gets a specific customer record.
		''' </summary>
		''' <param name="customerId">Unique customer object.</param>
		''' <returns>Customer object.</returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetCustomer(ByVal customerId As Integer) As Customer
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Customer" }
			request.Criteria = New CustomerCriteria With {.CustomerId = customerId}

			Dim response As CustomerResponse = ActionServiceClient.GetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetCustomers: RequestId and CorrelationId do not match.")
			End If

			Return response.Customer
		End Function

		''' <summary>
		''' Get customer object with all its orders.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>Customer object.</returns>
		Public Function GetCustomerWithOrders(ByVal customerId As Integer) As Customer
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Customer", "Orders" }
			request.Criteria = New CustomerCriteria With {.CustomerId = customerId}

			Dim response As CustomerResponse = ActionServiceClient.GetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetCustomersWithOrders: RequestId and CorrelationId do not match.")
			End If

			Return response.Customer
		End Function

		''' <summary>
		''' Adds a new customer to the database
		''' </summary>
		''' <param name="customer">Customer object to be added.</param>
		<DataObjectMethod(DataObjectMethodType.Insert)> _
		Public Sub AddCustomer(ByVal customer As Customer)
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Create"
			request.Customer = customer

			Dim response As CustomerResponse = ActionServiceClient.SetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("AddCustomer: RequestId and CorrelationId do not match.")
			End If

			' These messages are for public consumption. Includes validation errors.
			If response.Acknowledge = AcknowledgeType.Failure Then
				Throw New ApplicationException(response.Message)
			End If
		End Sub

		''' <summary>
		''' Updates a customer in the database.
		''' </summary>
		''' <param name="customer">Customer object with updated values.</param>
		<DataObjectMethod(DataObjectMethodType.Update)> _
		Public Sub UpdateCustomer(ByVal customer As Customer)
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Update"
			request.Customer = customer

			Dim response As CustomerResponse = ActionServiceClient.SetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("UpdateCustomer: RequestId and CorrelationId do not match.")
			End If

			' These messages are for public consumption. Includes validation errors.
			If response.Acknowledge = AcknowledgeType.Failure Then
				Throw New ApplicationException(response.Message)
			End If
		End Sub

		''' <summary>
		''' Deletes a customer from the database. A customer can only be deleted if no orders were placed.
		''' </summary>
		''' <param name="customerId">Unique customer identifier.</param>
		''' <returns>Number of orders deleted.</returns>
		<DataObjectMethod(DataObjectMethodType.Delete)> _
		Public Function DeleteCustomer(ByVal customerId As Integer) As Integer
			Dim request As New CustomerRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.Action = "Delete"
			request.Criteria = New CustomerCriteria With {.CustomerId = customerId}

			Dim response As CustomerResponse = ActionServiceClient.SetCustomers(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("DeleteCustomer: RequestId and CorrelationId do not match.")
			End If

			If response.Acknowledge = AcknowledgeType.Failure Then
				Throw New ApplicationException(response.Message)
			End If

			Return response.RowsAffected
		End Function
	End Class
End Namespace