Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq

Namespace BusinessObjects
	''' <summary>
	''' Class that holds information about an order.
	''' </summary>
	''' <remarks>
	''' Enterprise Design Pattern: Domain Model, Identity Field, Foreign Key Mapping.
	''' 
	''' This is where your business logic resides. In this example there are none.
	''' Another place for business logic is in the Facade.  
	''' For an example see CustomerFacade in the Facade layer.
	''' 
	''' The Domain Model Design Pattern states that domain objects incorporate 
	''' both behavior and data. Behavior may include simple or complex business logic.
	''' 
	''' The Identity Field Design Pattern saves the ID field in an object to maintain
	''' identity between an in-memory business object and that database rows.
	''' 
	''' The Foreign Key Mapping Design Pattern is implemented by the Order to Customer 
	''' reference. The pattern states that it maps an association between objects to 
	''' a foreign key reference between table. The CustomerId is the foreign key to the 
	''' Order. 
	''' </remarks>
	Public Class Order
		Inherits BusinessObject
		''' <summary>
		''' Default constructor for order class.
		''' </summary>
		Public Sub New()
			OrderDetails = New List(Of OrderDetail)()
			Version = _versionDefault
		End Sub

		''' <summary>
		''' Overloaded constructor for the order class.
		''' </summary>
		''' <param name="orderId">Unique identifier for the Order</param>
		''' <param name="orderDate">Date at which Order is placed.</param>
		''' <param name="requiredDate">Date at which Order is required.</param>
		''' <param name="freight">Freight (shipping) costs for the Order.</param>
		Public Sub New(ByVal orderId As Integer, ByVal orderDate As DateTime, ByVal requiredDate As DateTime, ByVal freight As Single)
			Me.New()
			Me.OrderId = orderId
			Me.OrderDate = orderDate
			Me.RequiredDate = requiredDate
			Me.Freight = freight
		End Sub

		''' <summary>
		''' Gets or sets unique identifier for the order.
		''' Enterprise Design Pattern: Identity field pattern.
		''' </summary>
        Private _orderId As Integer
        Public Property OrderId() As Integer
            Get
                Return _orderId
            End Get
            Set(ByVal value As Integer)
                _orderId = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the date at which the order is placed.
		''' </summary>
        Private _orderDate As DateTime
        Public Property OrderDate() As DateTime
            Get
                Return _orderDate
            End Get
            Set(ByVal value As DateTime)
                _orderDate = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the date at which delivery of the order is required.
		''' </summary>
        Private _requiredDate As DateTime
        Public Property RequiredDate() As DateTime
            Get
                Return _requiredDate
            End Get
            Set(ByVal value As DateTime)
                _requiredDate = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the freight (shipping) costs for this order.
		''' </summary>
        Private _freight As Single
        Public Property Freight() As Single
            Get
                Return _freight
            End Get
            Set(ByVal value As Single)
                _freight = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the customer associated with the order.
		''' Enterprise Design Pattern: Foreign Key Mapping. Customer is the parent.
		''' </summary>
        Private _customer As Customer
        Public Property Customer() As Customer
            Get
                Return _customer
            End Get
            Set(ByVal value As Customer)
                _customer = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the customer name associated with the order.
		''' </summary>
		''' public string CustomerName{ get; set; }

		''' <summary>
		''' Gets or sets a list of order details (line items) for the order.
		''' </summary>
        Private _orderDetails As IList(Of OrderDetail)
		Public Property OrderDetails() As IList(Of OrderDetail)
			Get
                Return _orderDetails
			End Get
            Set(ByVal value As IList(Of OrderDetail))
                _orderDetails = value
            End Set
		End Property

		''' <summary>
		''' Gets or sets version. Used in support of optimistic concurrency.
		''' </summary>
        Private _version As String
        Public Property Version() As String
            Get
                Return _version
            End Get
            Set(ByVal value As String)
                _version = value
            End Set
        End Property
	End Class
End Namespace
