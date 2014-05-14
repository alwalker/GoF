Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports System.Collections.ObjectModel

Imports WPFModel.BusinessModelObjects
Imports ActionServiceReference
Imports WPFModel.Provider

Namespace WPFModel.DataTransferObjectMapper
	''' <summary>
	''' Maps DTOs (Data Transfer Objects) to BOs (Businessmodel Objects) and vice versa.
	''' </summary>
	Friend NotInheritable Class Mapper
		''' <summary>
		''' Maps array of customer data transfer objects to customer model objects.
		''' </summary>
		''' <param name="customers">Array of customer data transfer objects.</param>
		''' <param name="provider">Provider.</param>
		''' <returns>Observable collection of customer models.</returns>
		Private Sub New()
		End Sub
		Friend Shared Function FromDataTransferObjects(ByVal customers() As Customer, ByVal provider As IProvider) As ObservableCollection(Of CustomerModel)
			If customers Is Nothing Then
				Return Nothing
			End If

			Dim list As List(Of CustomerModel) = customers.Select(Function(c) FromDataTransferObject(c, provider)).ToList()
			Return New ObservableCollection(Of CustomerModel)(list)
		End Function

		''' <summary>
		''' Maps single customer data transfer object to customer model.
		''' </summary>
		''' <param name="customer">The customer data transfer object.</param>
		''' <param name="provider">Provider.</param>
		''' <returns></returns>
		Friend Shared Function FromDataTransferObject(ByVal customer As Customer, ByVal provider As IProvider) As CustomerModel
			Dim customerModel As New CustomerModel(provider)

			customerModel.CustomerId = customer.CustomerId
			customerModel.Company = customer.Company
			customerModel.City = customer.City
			customerModel.Country = customer.Country
			customerModel.Orders = FromDataTransferObjects(customer.Orders)
			customerModel.Version = customer.Version

			Return customerModel
		End Function

		''' <summary>
		''' Maps array of customer data transfer objects to customer model objects.
		''' </summary>
		''' <param name="orders">Array of order data transfer objects.</param>
		''' <returns>Observable collection of order model objects.</returns>
		Friend Shared Function FromDataTransferObjects(ByVal orders() As Order) As ObservableCollection(Of OrderModel)
			If orders Is Nothing Then
				Return Nothing
			End If

			Dim list As List(Of OrderModel) = orders.Select(Function(o) FromDataTransferObject(o)).ToList()
			Return New ObservableCollection(Of OrderModel)(list)
		End Function

		''' <summary>
		''' Maps single order data transfer object to order model.
		''' </summary>
		''' <param name="order">Order data transfer object.</param>
		''' <returns>Order model object.</returns>
		Friend Shared Function FromDataTransferObject(ByVal order As Order) As OrderModel
			Return New OrderModel With {.OrderId = order.OrderId, .Freight = order.Freight, .OrderDate = order.OrderDate, .RequiredDate = order.RequiredDate, .OrderDetails = FromDataTransferObjects(order.OrderDetails), .Version = order.Version}
		End Function

		''' <summary>
		''' Maps arrary of order detail data transfer objects to observable collection of order details models.
		''' </summary>
		''' <param name="orderDetails">Array of order detail data transfer objects.</param>
		''' <returns>Observable collection of order detail models.</returns>
		Friend Shared Function FromDataTransferObjects(ByVal orderDetails() As OrderDetail) As ObservableCollection(Of OrderDetailModel)
			If orderDetails Is Nothing Then
				Return Nothing
			End If

			Dim list As List(Of OrderDetailModel) = orderDetails.Select(Function(o) FromDataTransferObject(o)).ToList()
			Return New ObservableCollection(Of OrderDetailModel)(list)
		End Function

		''' <summary>
		''' Maps order detail data transfer object to order model object.
		''' </summary>
		''' <param name="orderDetail">Order detail data transfer object.</param>
		''' <returns>Orderdetail model object.</returns>
		Friend Shared Function FromDataTransferObject(ByVal orderDetail As OrderDetail) As OrderDetailModel
			Return New OrderDetailModel With {.ProductName = orderDetail.ProductName, .Discount = orderDetail.Discount, .Quantity = orderDetail.Quantity, .UnitPrice = orderDetail.UnitPrice, .Version = orderDetail.Version}
		End Function

		''' <summary>
		''' Maps customer model object to customer data transfer object.
		''' </summary>
		''' <param name="customer">Customer model object.</param>
		''' <returns>Customer data transfer object.</returns>
		Friend Shared Function ToDataTransferObject(ByVal customer As CustomerModel) As Customer
			Return New Customer With {.CustomerId = customer.CustomerId, .Company = customer.Company, .City = customer.City, .Country = customer.Country, .Version = customer.Version}
		End Function
	End Class
End Namespace