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
	''' Product Data Transfer Object.
	''' 
	''' The purpose of the CustomerTransferObject is to facilitate transport of 
	''' customer business data in a serializable format. Business data is kept in 
	''' publicly accessible auto property members. This class has no methods. 
	''' </summary>
	''' <remarks>
	''' Pattern: Data Transfer Objects.
	''' 
	''' Data Transfer Objects are objects that transfer data between processes, but without behavior.
	''' </remarks>
	<DataContract(Name := "Product", Namespace := "http://www.yourcompany.com/types/")> _
	Public Class ProductDto
		''' <summary>
		''' Gets or sets the unique identifier for the product.
		''' The Identity Field Design Pattern. 
		''' </summary>
        Private _productId As Integer
        <DataMember()> Public Property ProductId() As Integer
            Get
                Return _productId
            End Get
            Set(ByVal value As Integer)
                _productId = value
            End Set
        End Property

		''' <summary>
		''' Getd or sets the product name.
		''' </summary>
        Private _productName As String
        <DataMember()> Public Property ProductName() As String
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
        <DataMember()> Public Property Weight() As String
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
        <DataMember()> Public Property UnitPrice() As Double
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
        <DataMember()> Public Property UnitsInStock() As Integer
            Get
                Return _unitsInStock
            End Get
            Set(ByVal value As Integer)
                _unitsInStock = value
            End Set
        End Property

		''' <summary>
		''' Gets category name under which product is categorized.
		''' </summary>
        Private _category As CategoryDto
        <DataMember()> Public Property Category() As CategoryDto
            Get
                Return _category
            End Get
            Set(ByVal value As CategoryDto)
                _category = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets version. Used in support of optimistic concurrency.
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

