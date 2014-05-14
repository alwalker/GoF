Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Cart
	''' <summary>
	''' A non-persistent shopping cart. It would be simple to create a
	''' shopping cart table in the database and make it persistent.
	''' </summary>
	''' <remarks>
	''' GoF Design Patterns: Strategy.
	''' Enterprise Design Patterns: Table Module, Data Table Gateway.
	''' The Strategy Pattern is the 'pluggability' of the different shipping methods. 
	''' </remarks>
	Public Class ShoppingCart
		' The shopping cart line items
		Private _items As IList(Of ShoppingCartItem) = New List(Of ShoppingCartItem)()

		' Totals
		Private _subTotal As Double
		Private _total As Double
		Private _shipping As Double

		' Pluggable shipping strategy
		Private _shippingStrategy As IShipping
		Private _shippingMethod As ShippingMethod

		' When cart is 'dirty' recalculations are required
		Private _isDirty As Boolean = False

		''' <summary>
		''' Default constructor. Sets standard shipping method as Fedex.
		''' </summary>
		Public Sub New()
			Me.New(ShippingMethod.Fedex)
		End Sub

		''' <summary>
		''' Constructor for shopping cart. 
		''' </summary>
		Public Sub New(ByVal shippingMethod As ShippingMethod)
			Me.ShippingMethod = shippingMethod
		End Sub

		''' <summary>
		''' Adds a product item to the shopping cart.
		''' </summary>
		''' <param name="id">Unique product identifier.</param>
		''' <param name="name">Product name.</param>
		''' <param name="quantity">Quantity.</param>
		''' <param name="unitPrice">Unit price of product.</param>
		Public Sub AddItem(ByVal id As Integer, ByVal name As String, ByVal quantity As Integer, ByVal unitPrice As Double)
			_isDirty = True

			For Each item As ShoppingCartItem In _items
				If item.Id = id Then
					item.Quantity += quantity
					Return
				End If
			Next item

			_items.Add(New ShoppingCartItem With {.Id = id, .Name = name, .Quantity = quantity, .UnitPrice = unitPrice})
		End Sub

		''' <summary>
		''' Removes a product item from the shopping cart.
		''' </summary>
		''' <param name="id">Unique product identifier.</param>
		Public Sub RemoveItem(ByVal id As Integer)
			For Each item As ShoppingCartItem In _items
				If item.Id = id Then
					_items.Remove(item)
					_isDirty = True
					Return
				End If
			Next item
		End Sub

		''' <summary>
		''' Updates quantity for a given product in shopping cart.
		''' </summary>
		''' <param name="id">Unique product identifier.</param>
		''' <param name="quantity">New quantity.</param>
		Public Sub UpdateQuantity(ByVal id As Integer, ByVal quantity As Integer)
			For Each item As ShoppingCartItem In _items
				If item.Id = id Then
					item.Quantity = quantity
					_isDirty = True
					Return
				End If
			Next item
		End Sub

		''' <summary>
		''' Recalculates the total, subtotals, and shipping costs.
		''' </summary>
		Public Sub ReCalculate()
			' No need to calculate if nothing was changed
			If (Not _isDirty) Then
				Return
			End If

			_subTotal = 0.0
			_shipping = 0.0

			For Each item As ShoppingCartItem In _items
				_subTotal += item.UnitPrice * item.Quantity
				_shipping += _shippingStrategy.EstimateShipping(item.UnitPrice, item.Quantity)
			Next item

			' Add subtotal and shipping to get total
			_total = _subTotal + _shipping

			_isDirty = False
		End Sub

		''' <summary>
		''' Gets subtotal for all items in the shopping cart. Recalculate if needed.
		''' </summary>
		Public ReadOnly Property SubTotal() As Double
			Get
				ReCalculate()
				Return _subTotal
			End Get
		End Property

		''' <summary>
		''' Gets total for all items in shopping cart. Recalculate if needed.
		''' </summary>
		Public ReadOnly Property Total() As Double
			Get
				ReCalculate()
				Return _total
			End Get
		End Property

		''' <summary>
		''' Gets shipping cost for all items in shopping cart. Recalculate if needed.
		''' </summary>
		Public ReadOnly Property Shipping() As Double
			Get
				ReCalculate()
				Return _shipping
			End Get
		End Property

		''' <summary>
		''' Gets datatable holding shopping cart data.
		''' </summary>
		Public ReadOnly Property Items() As IList(Of ShoppingCartItem)
			Get
				Return _items
			End Get
		End Property

		''' <summary>
		''' Gets or sets shipping method, which in turn sets the 'strategy', 
		''' i.e. the means at which products are shipped.
		''' This is the Strategy Design Pattern in action.
		''' </summary>
		Public Property ShippingMethod() As ShippingMethod
			Set(ByVal value As ShippingMethod)
				_shippingMethod = value

				Select Case _shippingMethod
					Case ShippingMethod.Fedex
						_shippingStrategy = New ShippingStrategyFedex()
					Case ShippingMethod.UPS
						_shippingStrategy = New ShippingStrategyUPS()
					Case Else
						_shippingStrategy = New ShippingStrategyUSPS()
				End Select
				_isDirty = True
			End Set

			Get
				Return _shippingMethod
			End Get
		End Property
	End Class
End Namespace

