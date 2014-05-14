using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

using WPFModel.BusinessModelObjects;
using WPFModel.ActionServiceReference;
using WPFModel.Provider;

namespace WPFModel.DataTransferObjectMapper
{
    /// <summary>
    /// Maps DTOs (Data Transfer Objects) to BOs (Businessmodel Objects) and vice versa.
    /// </summary>
    internal static class Mapper
    {
        /// <summary>
        /// Maps array of customer data transfer objects to customer model objects.
        /// </summary>
        /// <param name="customers">Array of customer data transfer objects.</param>
        /// <param name="provider">Provider.</param>
        /// <returns>Observable collection of customer models.</returns>
        internal static ObservableCollection<CustomerModel> FromDataTransferObjects(Customer[] customers, IProvider provider)
        {
            if (customers == null)
                return null;

            List<CustomerModel> list = customers.Select(c => FromDataTransferObject(c, provider)).ToList();
            return new ObservableCollection<CustomerModel>(list);
        }

        /// <summary>
        /// Maps single customer data transfer object to customer model.
        /// </summary>
        /// <param name="customer">The customer data transfer object.</param>
        /// <param name="provider">Provider.</param>
        /// <returns></returns>
        internal static CustomerModel FromDataTransferObject(Customer customer, IProvider provider)
        {
            CustomerModel customerModel = new CustomerModel(provider);

            customerModel.CustomerId = customer.CustomerId;
            customerModel.Company = customer.Company;
            customerModel.City = customer.City;
            customerModel.Country = customer.Country;
            customerModel.Orders = FromDataTransferObjects(customer.Orders);
            customerModel.Version = customer.Version;

            return customerModel;
        }

        /// <summary>
        /// Maps array of customer data transfer objects to customer model objects.
        /// </summary>
        /// <param name="orders">Array of order data transfer objects.</param>
        /// <returns>Observable collection of order model objects.</returns>
        internal static ObservableCollection<OrderModel> FromDataTransferObjects(Order[] orders)
        {
            if (orders == null)
                return null;

            List<OrderModel> list = orders.Select(o => FromDataTransferObject(o)).ToList();
            return new ObservableCollection<OrderModel>(list);
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
        /// Maps arrary of order detail data transfer objects to observable collection of order details models.
        /// </summary>
        /// <param name="orderDetails">Array of order detail data transfer objects.</param>
        /// <returns>Observable collection of order detail models.</returns>
        internal static ObservableCollection<OrderDetailModel> FromDataTransferObjects(OrderDetail[] orderDetails)
        {
            if (orderDetails == null)
                return null;

            List<OrderDetailModel> list = orderDetails.Select(o => FromDataTransferObject(o)).ToList();
            return new ObservableCollection<OrderDetailModel>(list);
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