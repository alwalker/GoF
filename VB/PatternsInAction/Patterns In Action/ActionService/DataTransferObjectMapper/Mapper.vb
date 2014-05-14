Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Imports Cart
Imports BusinessObjects
Imports ActionService.DataTransferObjects

Namespace ActionService.DataTransferObjectMapper
	''' <summary>
	''' Maps DTOs (Data Transfer Objects) to BOs (Business Objects) and vice versa.
	''' </summary>
	Public NotInheritable Class Mapper
		''' <summary>
		''' Transforms list of category BOs list of category DTOs.
		''' </summary>
		''' <param name="categories">List of categories BOs.</param>
		''' <returns>List of category DTOs.</returns>
		Private Sub New()
		End Sub
		Public Shared Function ToDataTransferObjects(ByVal categories As IEnumerable(Of Category)) As IList(Of CategoryDto)
			If categories Is Nothing Then
				Return Nothing
			End If
			Return categories.Select(Function(c) ToDataTransferObject(c)).ToList()
		End Function

		''' <summary>
		''' Transforms category BO to category DTO.
		''' </summary>
		''' <param name="category">Category BO.</param>
		''' <returns>Category DTO.</returns>
		Public Shared Function ToDataTransferObject(ByVal category As Category) As CategoryDto
			If category Is Nothing Then
				Return Nothing
			End If

			Return New CategoryDto With {.CategoryId = category.CategoryId, .Name = category.Name, .Description = category.Description, .Version = category.Version}
		End Function

		''' <summary>
		''' Transforms list of Product BOs to list of Product DTOs.
		''' </summary>
		''' <param name="products">List of Product BOs.</param>
		''' <returns>List of Product DTOs.</returns>
		Public Shared Function ToDataTransferObjects(ByVal products As IEnumerable(Of Product)) As IList(Of ProductDto)
			If products Is Nothing Then
				Return Nothing
			End If
			Return products.Select(Function(p) ToDataTransferObject(p)).ToList()
		End Function

		''' <summary>
		''' Transforms Product BO to Product DTO.
		''' </summary>
		''' <param name="product">Product BO.</param>
		''' <returns>Product DTO.</returns>
		Public Shared Function ToDataTransferObject(ByVal product As Product) As ProductDto
			If product Is Nothing Then
				Return Nothing
			End If

			Return New ProductDto With {.ProductId = product.ProductId, .ProductName = product.ProductName, .Category = ToDataTransferObject(product.Category), .UnitPrice = product.UnitPrice, .UnitsInStock = product.UnitsInStock, .Weight = product.Weight, .Version = product.Version}
		End Function

		''' <summary>
		''' Transforms list of Customer BOs to list of Customer DTOs.
		''' </summary>
		''' <param name="customers">List of Customer BOs.</param>
		''' <returns>List of Customer DTOs.</returns>
		Public Shared Function ToDataTransferObjects(ByVal customers As IEnumerable(Of Customer)) As IList(Of CustomerDto)
			If customers Is Nothing Then
				Return Nothing
			End If
			Return customers.Select(Function(c) ToDataTransferObject(c)).ToList()
		End Function

		''' <summary>
		''' Transforms Customer BO to Customer DTO.
		''' </summary>
		''' <param name="customer">Customer BO.</param>
		''' <returns>Customer DTO.</returns>
		Public Shared Function ToDataTransferObject(ByVal customer As Customer) As CustomerDto
			If customer Is Nothing Then
				Return Nothing
			End If

			Return New CustomerDto With {.CustomerId = customer.CustomerId, .Company = customer.Company, .Country = customer.Country, .City = customer.City, .Orders = ToDataTransferObjects(customer.Orders), .LastOrderDate = customer.LastOrderDate, .NumOrders = customer.NumOrders, .Version = customer.Version}
		End Function

		''' <summary>
		''' Transforms list of Order BOs to list of Order DTOs.
		''' </summary>
		''' <param name="orders">List of Order BOs.</param>
		''' <returns>List of Order DTOs.</returns>
		Public Shared Function ToDataTransferObjects(ByVal orders As IEnumerable(Of Order)) As OrderDto()
			If orders Is Nothing Then
				Return Nothing
			End If
			Return orders.Select(Function(o) ToDataTransferObject(o)).ToArray()
		End Function

		''' <summary>
		''' Transfers Order BO to Order DTO.
		''' </summary>
		''' <param name="order">Order BO.</param>
		''' <returns>Order DTO.</returns>
		Public Shared Function ToDataTransferObject(ByVal order As Order) As OrderDto
			If order Is Nothing Then
				Return Nothing
			End If

			Return New OrderDto With {.OrderId = order.OrderId, .Freight = order.Freight, .OrderDate = order.OrderDate, .Customer = ToDataTransferObject(order.Customer), .OrderDetails = ToDataTransferObjects(order.OrderDetails), .RequiredDate = order.RequiredDate, .Version = order.Version}
		End Function

		''' <summary>
		''' Transfers list of OrderDetail BOs to list of OrderDetail DTOs.
		''' </summary>
		''' <param name="orderDetails">List of OrderDetail BOs.</param>
		''' <returns>List of OrderDetail DTOs.</returns>
		Public Shared Function ToDataTransferObjects(ByVal orderDetails As IEnumerable(Of OrderDetail)) As OrderDetailDto()
			If orderDetails Is Nothing Then
				Return Nothing
			End If
			Return orderDetails.Select(Function(o) ToDataTransferObject(o)).ToArray()
		End Function

		''' <summary>
		''' Transfers OrderDetail BO to OrderDetail DTO.
		''' </summary>
		''' <param name="orderDetail">OrderDetail BO.</param>
		''' <returns>OrderDetail DTO.</returns>
		Public Shared Function ToDataTransferObject(ByVal orderDetail As OrderDetail) As OrderDetailDto
			If orderDetail Is Nothing Then
				Return Nothing
			End If

			Return New OrderDetailDto With {.ProductName = orderDetail.ProductName, .Discount = orderDetail.Discount, .Quantity = orderDetail.Quantity, .UnitPrice = orderDetail.UnitPrice, .Version = orderDetail.Version}
		End Function

		''' <summary>
		''' Transfers a Shopping Cart BO to Shopping Cart DTO.
		''' </summary>
		''' <param name="cart">Shopping Cart BO.</param>
		''' <returns>Shopping Cart DTO.</returns>
		Public Shared Function ToDataTransferObject(ByVal cart As ShoppingCart) As ShoppingCartDto
			If cart Is Nothing Then
				Return Nothing
			End If

			Return New ShoppingCartDto With {.Shipping = cart.Shipping, .SubTotal = cart.SubTotal, .Total = cart.Total, .ShippingMethod = cart.ShippingMethod.ToString(), .CartItems = ToDataTransferObject(cart.Items)}
		End Function

		''' <summary>
		''' Transfers list of Shopping Cart Item BOs to list of Shopping Cart Items DTOs.
		''' </summary>
		''' <param name="cartItems">List of Shopping Cart Items BOs.</param>
		''' <returns>List of Shopping Cart Items DTOs.</returns>
		Private Shared Function ToDataTransferObject(ByVal cartItems As IList(Of ShoppingCartItem)) As ShoppingCartItemDto()
			If cartItems Is Nothing Then
				Return Nothing
			End If
			Return cartItems.Select(Function(i) ToDataTransferObject(i)).ToArray()
		End Function

		''' <summary>
		''' Transfers Shopping Cart Item BO to Shopping Cart Item DTO.
		''' </summary>
		''' <param name="item">Shopping Cart Item BO.</param>
		''' <returns>Shopping Cart Item DTO.</returns>
		Private Shared Function ToDataTransferObject(ByVal item As ShoppingCartItem) As ShoppingCartItemDto
			Return New ShoppingCartItemDto With {.Id = item.Id, .Name = item.Name, .UnitPrice = item.UnitPrice, .Quantity = item.Quantity}
		End Function

		''' <summary>
		''' Transfers Customer DTO to Customer BO.
		''' </summary>
		''' <param name="c">Customer DTO.</param>
		''' <returns>Customer BO.</returns>
		Public Shared Function FromDataTransferObject(ByVal c As CustomerDto) As Customer
			If c Is Nothing Then
				Return Nothing
			End If

			Return New Customer With {.CustomerId = c.CustomerId, .Company = c.Company, .City = c.City, .Country = c.Country, .Version = c.Version}
		End Function

		''' <summary>
		''' Transfers Shopping Cart Item DTO to Shopping Cart Item BO.
		''' </summary>
		''' <param name="item">Shopping Cart Item DTO.</param>
		''' <returns>Shopping Cart Item BO.</returns>
		Public Shared Function FromDataTransferObject(ByVal item As ShoppingCartItemDto) As ShoppingCartItem
			Return New ShoppingCartItem With {.Id = item.Id, .Name = item.Name, .Quantity = item.Quantity, .UnitPrice = item.UnitPrice}
		End Function
	End Class
End Namespace
