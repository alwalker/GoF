Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Xml.Serialization
Imports System.Linq
Imports System.Web
Imports System.Xml.Linq

Imports System.Runtime.Serialization

Namespace ActionService.DataTransferObjects
	''' <summary>
	''' Order Data Transfer Object.
	''' 
	''' The purpose of the CustomerTransferObject is to facilitate transport of 
	''' customer business data in a serializable format. Business data is kept in 
	''' publicly accessible auto property members. This class has no methods. 
	''' </summary>
	''' <remarks>
	''' Pattern: Data Data Transfer Objects.
	''' 
	''' Data Transfer Objects are objects that transfer data between processes, but without behavior.
	''' </remarks>
	<DataContract(Name := "Order", Namespace := "http://www.yourcompany.com/types/")> _
	Public Class OrderDto
		''' <summary>
		''' Unique order identifier.
		''' The Identity Field Design Pattern. 
		''' </summary>
        Private _orderId As Integer
        <DataMember()> Public Property OrderId() As Integer
            Get
                Return _orderId
            End Get
            Set(ByVal value As Integer)
                _orderId = value
            End Set
        End Property

		''' <summary>
		''' Date the order is placed.
		''' </summary>
        Private _orderDate As DateTime
        <DataMember()> Public Property OrderDate() As DateTime
            Get
                Return _orderDate
            End Get
            Set(ByVal value As DateTime)
                _orderDate = value
            End Set
        End Property

		''' <summary>
		''' Date the order is required for delivery.
		''' </summary>
        Private _requiredDate As DateTime
        <DataMember()> Public Property RequiredDate() As DateTime
            Get
                Return _requiredDate
            End Get
            Set(ByVal value As DateTime)
                _requiredDate = value
            End Set
        End Property

		''' <summary>
		''' Freight or shipping costs for the order.
		''' </summary>
        Private _freight As Single
        <DataMember()> Public Property Freight() As Single
            Get
                Return _freight
            End Get
            Set(ByVal value As Single)
                _freight = value
            End Set
        End Property

		''' <summary>
		''' List of order details (line items) for the order.
		''' </summary>
        Private _orderDetails As OrderDetailDto()
        <DataMember()> Public Property OrderDetails() As OrderDetailDto()
            Get
                Return _orderDetails
            End Get
            Set(ByVal value As OrderDetailDto())
                _orderDetails = value
            End Set
        End Property


		''' <summary>
		''' Not used yet
		''' </summary>
        Private _customer As CustomerDto
        <DataMember()> Public Property Customer() As CustomerDto
            Get
                Return _customer
            End Get
            Set(ByVal value As CustomerDto)
                _customer = value
            End Set
        End Property

		''' <summary>
		''' Version number. Used in optimistic concurrency.
		''' </summary>
        Private _version As String
        <DataMember()> Public Property Version() As String
            Get
                Return _version
            End Get
            Set(ByVal value As String)
                _version = value
            End Set
        End Property
	End Class
End Namespace