using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects;

namespace DataObjects
{
    /// <summary>
    /// Defines methods to access orders and order details.
    /// This is a database-independent interface. The implementations will 
    /// be database specific.
    /// </summary>
    public interface IOrderDao
    {
        /// <summary>
        /// Gets customers with order statistics in given sort order.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of customers with order statistics.</returns>
        IList<Customer> GetOrderStatistics(IList<Customer> customers, string sortExpression);

        /// <summary>
        /// Gets a list of customers with order summary statistics.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <returns>Customer list with order summary statistics.</returns>
        IList<Customer> GetOrderStatistics(IList<Customer> customers);

        /// <summary>
        /// Gets all orders for a customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>List of orders.</returns>
        IList<Order> GetOrders(int customerId);

        /// <summary>
        /// Gets a list of orders placed within a date range.
        /// </summary>
        /// <param name="dateFrom">Date range begin date.</param>
        /// <param name="dateThru">Date range end date.</param>
        /// <returns>List of orders.</returns>
        IList<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru);

        /// <summary>
        /// Gets a list of order details for a given order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>List of order details.</returns>
        IList<OrderDetail> GetOrderDetails(int orderId);

        /// <summary>
        /// Gets an specific order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>Order.</returns>
        Order GetOrder(int orderId);
    }
}
