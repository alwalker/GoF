using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WindowsFormsModel.BusinessObjects;
using WindowsFormsModel.ActionServiceReference;

namespace WindowsFormsModel.DataTransferObjectMapper
{
    /// <summary>
    /// Static class. Maps data transfer objects to model objects and vice versa.
    /// </summary>
    internal static class Mapper
    {
        /// <summary>
        /// Maps array of customer data transfer objects to customer model objects.
        /// </summary>
        /// <param name="customers">Array of customer data transfer objects.</param>
        /// <returns>List of customer models.</returns>
        internal static IList<CustomerModel> FromDataTransferObjects(Customer[] customers)
        {
            if (customers == null)
                return null;

            return customers.Select(c => FromDataTransferObject(c)).ToList();
        }

        /// <summary>
        /// Maps single customer data transfer object to customer model.
        /// </summary>
        /// <param name="customer">Customer data transfer object.</param>
        /// <returns>Customer model object.</returns>
        internal static CustomerModel FromDataTransferObject(Customer customer)
        {
            return new CustomerModel
            {
                Company = customer.Company,
                City = customer.City,
                Country = customer.Country,
                CustomerId = customer.CustomerId,
                Orders = FromDataTransferObjects(customer.Orders),
                Version = customer.Version
            };
        }

        /// <summary>
        /// Maps array of customer data transfer objects to customer model objects.
        /// </summary>
        /// <param name="orders">Array of order data transfer objects.</param>
        /// <returns>List of order model objects.</returns>
        internal static IList<OrderModel> FromDataTransferObjects(Order[] orders)
        {
            if (orders == null)
                return null;

            return orders.Select(o => FromDataTransferObject(o)).ToList();
        }

        /// <summary>
        /// Maps single order data transfer object to order model.
        /// </summary>
        /// <param name="order">Order data transfer object.</param>
        /// <returns>Order model object.</returns>
        internal static OrderModel FromDataTransferObject(Order order)
        {
            return new OrderModel
            {
                OrderId = order.OrderId,
                Freight = order.Freight,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                OrderDetails = FromDataTransferObjects(order.OrderDetails),
                Version = order.Version
            };
        }

        /// <summary>
        /// Maps arrary of order detail data transfer objects to list of order details models.
        /// </summary>
        /// <param name="orderDetails">Array of order detail data transfer objects.</param>
        /// <returns>List of order detail models.</returns>
        internal static IList<OrderDetailModel> FromDataTransferObjects(OrderDetail[] orderDetails)
        {
            if (orderDetails == null)
                return null;

            return orderDetails.Select(o => FromDataTransferObject(o)).ToList();
        }

        /// <summary>
        /// Maps order detail data transfer object to order model object.
        /// </summary>
        /// <param name="orderDetail">Order detail data transfer object.</param>
        /// <returns>Orderdetail model object.</returns>
        internal static OrderDetailModel FromDataTransferObject(OrderDetail orderDetail)
        {
            return new OrderDetailModel
            {
                ProductName = orderDetail.ProductName,
                Discount = orderDetail.Discount,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.UnitPrice,
                Version = orderDetail.Version
            };
        }

        /// <summary>
        /// Maps customer model object to customer data transfer object.
        /// </summary>
        /// <param name="customer">Customer model object.</param>
        /// <returns>Customer data transfer object.</returns>
        internal static Customer ToDataTransferObject(CustomerModel customer)
        {
            return new Customer
            {
                CustomerId = customer.CustomerId,
                Company = customer.Company,
                City = customer.City,
                Country = customer.Country,
                Version = customer.Version
            };
        }
    }
}
