Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Configuration
Imports System.Data.Linq
Imports System.Data.Linq.Mapping

Imports DataObjects.Linq.EntityMapper
Imports BusinessObjects
Imports DataObjects.Linq

Namespace DataObjects.Linq.LinqImplementation
	''' <summary>
	''' Linq-to-Sql implementation of the ICustomerDao interface.
	''' </summary>
	Public Class LinqCustomerDao
		Implements ICustomerDao
		''' <summary>
		''' Gets list of customers in default sort order.
		''' </summary>
		''' <returns>List of customers.</returns>
		Public Function GetCustomers() As IList(Of Customer) Implements ICustomerDao.GetCustomers
			Return GetCustomers("CustomerId ASC")
		End Function

		''' <summary>
		''' Gets list of customers in given sortorder.
		''' </summary>
		''' <param name="sortExpression">The required sort order.</param>
		''' <returns>List of customers.</returns>
		Public Function GetCustomers(ByVal sortExpression As String) As IList(Of Customer) Implements ICustomerDao.GetCustomers
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Dim query As IQueryable(Of CustomerEntity) = db.CustomerEntities

				If sortExpression.Length > 0 Then
					Dim sort() As String = sortExpression.Split(" "c)
					Dim sortColumn As String = sort(0)
					Dim sortOrder As String = sort(1)

					Select Case sortColumn
						Case "CustomerId"
							If sortOrder = "ASC" Then
								query = query.OrderBy(Function(c) c.CustomerId)
							Else
								query = query.OrderByDescending(Function(c) c.CustomerId)
							End If
						Case "CompanyName"
							If sortOrder = "ASC" Then
								query = query.OrderBy(Function(c) c.CompanyName)
							Else
								query = query.OrderByDescending(Function(c) c.CompanyName)
							End If
						Case "City"
							If sortOrder = "ASC" Then
								query = query.OrderBy(Function(c) c.City)
							Else
								query = query.OrderByDescending(Function(c) c.City)
							End If
						Case "Country"
							If sortOrder = "ASC" Then
								query = query.OrderBy(Function(c) c.Country)
							Else
								query = query.OrderByDescending(Function(c) c.Country)
							End If
					End Select
				End If
				Return query.Select(Function(c) Mapper.ToBusinessObject(c)).ToList()
			End Using
		End Function

		''' <summary>
		''' Gets a customer given a customer identifier.
		''' </summary>
		''' <param name="customerId">The customer identifier.</param>
		''' <returns>The customer.</returns>
		Public Function GetCustomer(ByVal customerId As Integer) As Customer Implements ICustomerDao.GetCustomer
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return Mapper.ToBusinessObject(db.CustomerEntities.SingleOrDefault(Function(p) p.CustomerId = customerId))
			End Using
		End Function

		''' <summary>
		''' Gets customer given an order.
		''' </summary>
		''' <param name="orderId">The identifier for the order for which customer is requested.</param>
		''' <returns>The customer.</returns>
		Public Function GetCustomerByOrder(ByVal orderId As Integer) As Customer Implements ICustomerDao.GetCustomerByOrder
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Return Mapper.ToBusinessObject(db.CustomerEntities.SelectMany(Function(c) db.OrderEntities.Where(Function(o) c.CustomerId = o.CustomerId AndAlso o.OrderId = orderId), Function(c, o) c).SingleOrDefault(Function(c) True))
			End Using
		End Function

		''' <summary>
		''' Inserts a new customer record to the database.
		''' </summary>
		''' <param name="customer">The customer to be inserted.</param>
		Public Sub InsertCustomer(ByVal customer As Customer) Implements ICustomerDao.InsertCustomer
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Try
					Dim entity As CustomerEntity = Mapper.ToEntity(customer)
					db.CustomerEntities.InsertOnSubmit(entity)
					db.SubmitChanges()

					' update business object with new version and id
					customer.CustomerId = entity.CustomerId
					customer.Version = VersionConverter.ToString(entity.Version)
				Catch e1 As ChangeConflictException
					Throw New Exception("A change to customer record was made before your changes.")
				End Try
			End Using
		End Sub

		''' <summary>
		''' Updates a customer record in the database.
		''' </summary>
		''' <param name="customer">The customer with updated values.</param>
		''' <returns>Number of rows affected.</returns>
		Public Function UpdateCustomer(ByVal customer As Customer) As Integer Implements ICustomerDao.UpdateCustomer
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Try
					Dim entity As CustomerEntity = Mapper.ToEntity(customer)
					db.CustomerEntities.Attach(entity, True)
					db.SubmitChanges()

					' Update business object with new version
					customer.Version = VersionConverter.ToString(entity.Version)

					Return 1
				Catch e1 As ChangeConflictException
					Throw New Exception("A change to customer record was made before your changes.")
				Catch
					Return 0
				End Try

			End Using
		End Function

		''' <summary>
		''' Deletes a customer record from the database.
		''' </summary>
		''' <param name="customer">The customer to be deleted.</param>
		''' <returns>Number of rows affected.</returns>
		Public Function DeleteCustomer(ByVal customer As Customer) As Integer Implements ICustomerDao.DeleteCustomer
			Using db As ActionDataContext = DataContextFactory.CreateContext()
				Try
					Dim entity As CustomerEntity = Mapper.ToEntity(customer)
					db.CustomerEntities.Attach(entity, False)
					db.CustomerEntities.DeleteOnSubmit(entity)
					db.SubmitChanges()

					Return 1
				Catch e1 As ChangeConflictException
					Throw New Exception("A change to customer record was made before your changes.")
				Catch e2 As Exception
					Return 0
				End Try
			End Using
		End Function
	End Class
End Namespace
