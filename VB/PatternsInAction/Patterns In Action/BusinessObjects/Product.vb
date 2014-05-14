Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Linq

Namespace BusinessObjects
	''' <summary>
	''' Class that holds product information.
	''' </summary>
	''' <remarks>
	''' Enterprise Design Pattern: Domain Model, Identity Field.
	''' 
	''' This is where your business logic resides. In this example there are none.
	''' Another place for business logic and business rules is in the Facade.  
	''' For an example see CustomerFacade in the Facade layer.
	''' 
	''' The Domain Model Design Pattern states that domain objects incorporate 
	''' both behavior and data. Behavior may include simple or complex business logic.
	''' 
	''' The Identity Field Design Pattern saves the ID field in an object to maintain
	''' identity between an in-memory business object and that database rows.
	''' </remarks>
	Public Class Product
		Inherits BusinessObject
		''' <summary>
		''' Default constructor for product.
		''' </summary>
		Public Sub New()
			Version = _versionDefault
		End Sub

		''' <summary>
		''' Overloaded constructor for product
		''' </summary>
		''' <param name="productId">Unique identifier for Product</param>
		''' <param name="productName">Name of Product.</param>
		''' <param name="weight">Weight of Product.</param>
		''' <param name="unitPrice">Unit price of Product in US$.</param>
		''' <param name="unitsInStock">Product units in stock.</param>
		Public Sub New(ByVal productId As Integer, ByVal productName As String, ByVal weight As String, ByVal unitPrice As Double, ByVal unitsInStock As Integer)
			Me.New()
			Me.ProductId = productId
			Me.ProductName = productName
			Me.Weight = weight
			Me.UnitPrice = unitPrice
			Me.UnitsInStock = unitsInStock
		End Sub

		''' <summary>
		''' Gets or sets the unique identifier for the product.
		''' The Identity Field Design Pattern. 
		''' </summary>
        Private _productId As Integer
        Public Property ProductId() As Integer
            Get
                Return _productId
            End Get
            Set(ByVal value As Integer)
                _productId = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the product name.
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
		''' Gets or sets the weight of the product.
		''' </summary>
        Private _weight As String
        Public Property Weight() As String
            Get
                Return _weight
            End Get
            Set(ByVal value As String)
                _weight = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the unit price of the product in US$.
		''' </summary>
        Private _unitPrice As Double
        Public Property UnitPrice() As Double
            Get
                Return _unitPrice
            End Get
            Set(ByVal value As Double)
                _unitPrice = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets units in stock for the product.
		''' </summary>
        Private _unitsInStock As Integer
        Public Property UnitsInStock() As Integer
            Get
                Return _unitsInStock
            End Get
            Set(ByVal value As Integer)
                _unitsInStock = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets the product category under which product is categorized.
		''' </summary>
        Private _category As Category
        Public Property Category() As Category
            Get
                Return _category
            End Get
            Set(ByVal value As Category)
                _category = value
            End Set
        End Property

		''' <summary>
		''' Gets category name under which product is categorized.
		''' </summary>
		'''public string CategoryName{ get; set; }

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
