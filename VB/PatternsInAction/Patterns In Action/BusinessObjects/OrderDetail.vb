Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq

Namespace BusinessObjects
	''' <summary>
	''' Class that holds order details (line items) for an order.
	''' </summary>
	''' <remarks>
	''' Enterprise Design Pattern: Domain Model.
	''' 
	''' This is where your business logic resides. In this example there are none.
	''' Another place for business logic and business rules is in the Facade.  
	''' For an example see CustomerFacade in the Facade layer.
	''' 
	''' The Domain Model Design Pattern states that domain objects incorporate 
	''' both behavior and data. Behavior may include simple or complex business logic.
	''' </remarks>
	Public Class OrderDetail
		Inherits BusinessObject
		''' <summary>
		''' Default constructor for Order Detail.
		''' </summary>
		Public Sub New()
			Version = _versionDefault
		End Sub

		''' <summary>
		''' Overloaded  constructor for Order Detail.
		''' </summary>
		''' <param name="productName">Product name of Order Detail.</param>
		''' <param name="quantity">Quantity ordered.</param>
		''' <param name="unitPrice">Unit price of product at the time order is placed.</param>
		''' <param name="discount">Discount applied to unit price of product.</param>
		''' <param name="order">Order that Order Detail is part of.</param>
		Public Sub New(ByVal productName As String, ByVal quantity As Integer, ByVal unitPrice As Single, ByVal discount As Single, ByVal order As Order)
			Me.New()
			Me.ProductName = productName
			Me.Quantity = quantity
			Me.UnitPrice = unitPrice
			Me.Discount = discount
			Order = order ' The parent object
		End Sub

		''' <summary>
		''' Get or set Product name of Order Detail (line item).
		''' </summary>
        Private _productName As String
        Public Property ProductName() As String
            Get
                Return _productName
            End Get
            Set(ByVal value As String)
                _productName = value
            End Set
        End Property

		''' <summary>
		''' Get or set quantity of Products ordered.
		''' </summary>
        Private _quantity As Integer
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
            End Set
        End Property

		''' <summary>
		''' Get or set unit price of Product in US$.
		''' </summary>
        Private _unitPrice As Single
        Public Property UnitPrice() As Single
            Get
                Return _unitPrice
            End Get
            Set(ByVal value As Single)
                _unitPrice = value
            End Set
        End Property

		''' <summary>
		''' Get or set discount applied to unit price.
		''' </summary>
        Private _discount As Single
        Public Property Discount() As Single
            Get
                Return _discount
            End Get
            Set(ByVal value As Single)
                _discount = value
            End Set
        End Property

		''' <summary>
		''' Get or set the Order of which this Order Detail is part of.
		''' </summary>
        Private _order As Order
        Public Property Order() As Order
            Get
                Return _order
            End Get
            Set(ByVal value As Order)
                _order = value
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
