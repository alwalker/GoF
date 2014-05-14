Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Xml.Linq
Imports System.ComponentModel

Imports ActionServiceReference

Namespace ASPNETWebApplication.Controllers
	''' <summary>
	''' Controller for products.
	''' </summary>
	''' <remarks>
	''' MV Patterns: Model View Controller Pattern.
	''' This is an 'informal' implementation of the MVC pattern.
	''' </remarks>
	<DataObject(True)> _
	Public Class ProductController
		Inherits ControllerBase
		''' <summary>
		''' Gets a list of product categories.
		''' </summary>
		''' <returns>List of categories.</returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetCategories() As IList(Of Category)
			Dim request As New ProductRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Categories" }

			Dim response As ProductResponse = ActionServiceClient.GetProducts(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetCategories: RequestId and CorrelationId do not match.")
			End If

			Return response.Categories
		End Function

		''' <summary>
		''' Gets a list of products.
		''' </summary>
		''' <param name="categoryId">The category for which products are requested.</param>
		''' <param name="sortExpression">Sort order in which products are returned.</param>
		''' <returns>List of products.</returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetProducts(ByVal categoryId As Integer, ByVal sortExpression As String) As IList(Of Product)
			Dim request As New ProductRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Products" }
			request.Criteria = New ProductCriteria With {.CategoryId = categoryId, .SortExpression = sortExpression}

			Dim response As ProductResponse = ActionServiceClient.GetProducts(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetProductsByCategory: RequestId and CorrelationId do not match.")
			End If

			Return response.Products

		End Function

		''' <summary>
		''' Gets a specific product.
		''' </summary>
		''' <param name="productId">Unique product identifier.</param>
		''' <returns>The requested product.</returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetProduct(ByVal productId As Integer) As Product
			Dim request As New ProductRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Product" }
			request.Criteria = New ProductCriteria With {.ProductId = productId}

			Dim response As ProductResponse = ActionServiceClient.GetProducts(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("GetProductsByCategory: RequestId and CorrelationId do not match.")
			End If

			Return response.Product

		End Function

		''' <summary>
		''' Searches for products.
		''' </summary>
		''' <param name="productName">Product name.</param>
		''' <param name="priceRangeId">Price range identifier.</param>
		''' <param name="sortExpression">Sort order in which products are returned.</param>
		''' <returns>List of products that meet the search criteria.</returns>
		Public Function SearchProducts(ByVal productName As String, ByVal priceRangeId As Integer, ByVal sortExpression As String) As IList(Of Product)
			Dim request As New ProductRequest()
			request.RequestId = NewRequestId
			request.AccessToken = AccessToken
			request.ClientTag = ClientTag

			request.LoadOptions = New String() { "Products", "Search" }

			Dim priceFrom As Double = -1
			Dim priceThru As Double = -1
			If priceRangeId > 0 Then
				Dim pri As PriceRangeItem = PriceRange.List(priceRangeId)
				priceFrom = pri.RangeFrom
				priceThru = pri.RangeThru
			End If

			request.Criteria = New ProductCriteria With {.ProductName = productName, .PriceFrom = priceFrom, .PriceThru = priceThru, .SortExpression = sortExpression}

			Dim response As ProductResponse = ActionServiceClient.GetProducts(request)

			If request.RequestId <> response.CorrelationId Then
				Throw New ApplicationException("SearchProducts: Request and CorrelationId do not match.")
			End If

			Return response.Products
		End Function

		''' <summary>
		''' Gets a list of price ranges.
		''' </summary>
		''' <returns></returns>
		<DataObjectMethod(DataObjectMethodType.Select)> _
		Public Function GetProductPriceRange() As IList(Of PriceRangeItem)
			Return PriceRange.List
		End Function
	End Class

	''' <summary>
	''' A utility class with a list of price ranges. The price ranges are  
	''' used in building search criteria for the product catalog. 
	''' </summary>
	Public NotInheritable Class PriceRange
		Private Shared _list As IList(Of PriceRangeItem) = Nothing

		''' <summary>
		''' Static constructor for PriceRange.
		''' </summary>
		Private Sub New()
		End Sub
		Shared Sub New()
			_list = New List(Of PriceRangeItem)()

			_list.Add(New PriceRangeItem(0, 0, 0, ""))
			_list.Add(New PriceRangeItem(1, 0, 50, "$0 - $50"))
			_list.Add(New PriceRangeItem(2, 51, 100, "$51 - $100"))
			_list.Add(New PriceRangeItem(3, 101, 250, "$101 - $250"))
			_list.Add(New PriceRangeItem(4, 251, 1000, "$251 - $1,000"))
			_list.Add(New PriceRangeItem(5, 1001, 2000, "$1,001 - $2,000"))
			_list.Add(New PriceRangeItem(6, 2001, 10000, "$2,001 - $10,000"))
		End Sub

		''' <summary>
		''' Gets the list of price ranges.
		''' </summary>
		Public Shared ReadOnly Property List() As IList(Of PriceRangeItem)
			Get
				Return _list
			End Get
		End Property
	End Class

	''' <summary>
	''' A PriceRange item used in the PriceRange list.  PriceRanges are used for 
	''' searching the product catalog. 
	''' </summary>
	Public Class PriceRangeItem
		''' <summary>
		''' Constructor for PriceRangeItem.
		''' </summary>
		''' <param name="rangeId">Unique identifier for the price range.</param>
		''' <param name="rangeFrom">Lower end of the price range.</param>
		''' <param name="rangeThru">Higher end of the price range.</param>
		''' <param name="rangeText">Easy-to-read form of the price range.</param>
		Public Sub New(ByVal rangeId As Integer, ByVal rangeFrom As Double, ByVal rangeThru As Double, ByVal rangeText As String)
			Me.RangeId = rangeId
			Me.RangeFrom = rangeFrom
			Me.RangeThru = rangeThru
			Me.RangeText = rangeText
		End Sub

		''' <summary>
		''' Gets the unique PriceRange identifier.
		''' </summary>
        Private _rangeId As Integer
        Public Property RangeId() As Integer
            Get
                Return _rangeId
            End Get
            Private Set(ByVal value As Integer)
                _rangeId = value
            End Set
        End Property

		''' <summary>
		''' Gets the low end of the PriceRange item.
		''' </summary>
        Private _rangeFrom As Double
        Public Property RangeFrom() As Double
            Get
                Return _rangeFrom
            End Get
            Private Set(ByVal value As Double)
                _rangeFrom = value
            End Set
        End Property

		''' <summary>
		''' Gets the high end of the PriceRange item.
		''' </summary>
        Private _rangeThru As Double
        Public Property RangeThru() As Double
            Get
                Return _rangeThru
            End Get
            Private Set(ByVal value As Double)
                _rangeThru = value
            End Set
        End Property

		''' <summary>
		''' Gets an easy-to-read form of the PriceRange item.
		''' </summary>
        Private _rangeText As String
        Public Property RangeText() As String
            Get
                Return _rangeText
            End Get
            Private Set(ByVal value As String)
                _rangeText = value
            End Set
        End Property
	End Class
End Namespace
