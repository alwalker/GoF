using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Cart;
using BusinessObjects;
using ActionService.DataTransferObjects;

namespace ActionService.DataTransferObjectMapper
{
    /// <summary>
    /// Maps DTOs (Data Transfer Objects) to BOs (Business Objects) and vice versa.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Transforms list of category BOs list of category DTOs.
        /// </summary>
        /// <param name="categories">List of categories BOs.</param>
        /// <returns>List of category DTOs.</returns>
        public static IList<CategoryDto> ToDataTransferObjects(IEnumerable<Category> categories)
        {
            if (categories == null) return null;
            return categories.Select(c => ToDataTransferObject(c)).ToList();
        }

        /// <summary>
        /// Transforms category BO to category DTO.
        /// </summary>
        /// <param name="category">Category BO.</param>
        /// <returns>Category DTO.</returns>
        public static CategoryDto ToDataTransferObject(Category category)
        {
            if (category == null) return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                Version = category.Version
            };
        }

        /// <summary>
        /// Transforms list of Product BOs to list of Product DTOs.
        /// </summary>
        /// <param name="products">List of Product BOs.</param>
        /// <returns>List of Product DTOs.</returns>
        public static IList<ProductDto> ToDataTransferObjects(IEnumerable<Product> products)
        {
            if (products == null) return null;
            return products.Select(p => ToDataTransferObject(p)).ToList();
        }

        /// <summary>
        /// Transforms Product BO to Product DTO.
        /// </summary>
        /// <param name="product">Product BO.</param>
        /// <returns>Product DTO.</returns>
        public static ProductDto ToDataTransferObject(Product product)
        {
            if (product == null) return null;

            return new ProductDto
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Category = ToDataTransferObject(product.Category),
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Weight = product.Weight,
                Version = product.Version
            };
        }

        /// <summary>
        /// Transforms list of Customer BOs to list of Customer DTOs.
        /// </summary>
        /// <param name="customers">List of Customer BOs.</param>
        /// <returns>List of Customer DTOs.</returns>
        public static IList<CustomerDto> ToDataTransferObjects(IEnumerable<Customer> customers)
        {
            if (customers == null) return null;
            return customers.Select(c => ToDataTransferObject(c)).ToList();
        }

        /// <summary>
        /// Transforms Customer BO to Customer DTO.
        /// </summary>
        /// <param name="customer">Customer BO.</param>
        /// <returns>Customer DTO.</returns>
        public static CustomerDto ToDataTransferObject(Customer customer)
        {
            if (customer == null) return null;

            return new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Company = customer.Company,
                Country = customer.Country,
                City = customer.City,
                Orders = ToDataTransferObjects(customer.Orders),
                LastOrderDate = customer.LastOrderDate,
                NumOrders = customer.NumOrders,
                Version = customer.Version
            };
        }

        /// <summary>
        /// Transforms list of Order BOs to list of Order DTOs.
        /// </summary>
        /// <param name="orders">List of Order BOs.</param>
        /// <returns>List of Order DTOs.</returns>
        public static OrderDto[] ToDataTransferObjects(IEnumerable<Order> orders)
        {
            if (orders == null) return null;
            return orders.Select(o => ToDataTransferObject(o)).ToArray();
        }

        /// <summary>
        /// Transfers Order BO to Order DTO.
        /// </summary>
        /// <param name="order">Order BO.</param>
        /// <returns>Order DTO.</returns>
        public static OrderDto ToDataTransferObject(Order order)
        {
            if (order == null) return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                Freight = order.Freight,
                OrderDate = order.OrderDate,
                Customer = ToDataTransferObject(order.Customer),
                OrderDetails = ToDataTransferObjects(order.OrderDetails),
                RequiredDate = order.RequiredDate,
                Version = order.Version
            };
        }

        /// <summary>
        /// Transfers list of OrderDetail BOs to list of OrderDetail DTOs.
        /// </summary>
        /// <param name="orderDetails">List of OrderDetail BOs.</param>
        /// <returns>List of OrderDetail DTOs.</returns>
        public static OrderDetailDto[] ToDataTransferObjects(IEnumerable<OrderDetail> orderDetails)
        {
            if (orderDetails == null) return null;
            return orderDetails.Select(o => ToDataTransferObject(o)).ToArray();
        }

        /// <summary>
        /// Transfers OrderDetail BO to OrderDetail DTO.
        /// </summary>
        /// <param name="orderDetail">OrderDetail BO.</param>
        /// <returns>OrderDetail DTO.</returns>
        public static OrderDetailDto ToDataTransferObject(OrderDetail orderDetail)
        {
            if (orderDetail == null) return null;

            return new OrderDetailDto
            {
                ProductName = orderDetail.ProductName,
                Discount = orderDetail.Discount,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice,
                Version = orderDetail.Version
            };
        }

        /// <summary>
        /// Transfers a Shopping Cart BO to Shopping Cart DTO.
        /// </summary>
        /// <param name="cart">Shopping Cart BO.</param>
        /// <returns>Shopping Cart DTO.</returns>
        public static ShoppingCartDto ToDataTransferObject(ShoppingCart cart)
        {
            if (cart == null) return null;

            return new ShoppingCartDto
            {
                Shipping = cart.Shipping,
                SubTotal = cart.SubTotal,
                Total = cart.Total,
                ShippingMethod = cart.ShippingMethod.ToString(),
                CartItems = ToDataTransferObject(cart.Items)
            };
        }

        /// <summary>
        /// Transfers list of Shopping Cart Item BOs to list of Shopping Cart Items DTOs.
        /// </summary>
        /// <param name="cartItems">List of Shopping Cart Items BOs.</param>
        /// <returns>List of Shopping Cart Items DTOs.</returns>
        private static ShoppingCartItemDto[] ToDataTransferObject(IList<ShoppingCartItem> cartItems)
        {
            if (cartItems == null) return null;
            return cartItems.Select(i => ToDataTransferObject(i)).ToArray();
        }

        /// <summary>
        /// Transfers Shopping Cart Item BO to Shopping Cart Item DTO.
        /// </summary>
        /// <param name="item">Shopping Cart Item BO.</param>
        /// <returns>Shopping Cart Item DTO.</returns>
        private static ShoppingCartItemDto ToDataTransferObject(ShoppingCartItem item)
        {
            return new ShoppingCartItemDto
            {
                Id = item.Id,
                Name = item.Name,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            };
        }

        /// <summary>
        /// Transfers Customer DTO to Customer BO.
        /// </summary>
        /// <param name="c">Customer DTO.</param>
        /// <returns>Customer BO.</returns>
        public static Customer FromDataTransferObject(CustomerDto c)
        {
            if (c == null) return null;

            return new Customer
            {
                CustomerId = c.CustomerId,
                Company = c.Company,
                City = c.City,
                Country = c.Country,
                Version = c.Version
            };
        }

        /// <summary>
        /// Transfers Shopping Cart Item DTO to Shopping Cart Item BO.
        /// </summary>
        /// <param name="item">Shopping Cart Item DTO.</param>
        /// <returns>Shopping Cart Item BO.</returns>
        public static ShoppingCartItem FromDataTransferObject(ShoppingCartItemDto item)
        {
            return new ShoppingCartItem
            {
                Id = item.Id,
                Name = item.Name,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };
        }
    }
}
