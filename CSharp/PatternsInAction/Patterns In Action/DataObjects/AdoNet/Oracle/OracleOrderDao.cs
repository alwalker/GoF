using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BusinessObjects;

namespace DataObjects.AdoNet.Oracle
{
    /// <summary>
    /// Oracle specific data access object that handles data access
    /// of customer related orders and order details. The details are stubbed out (in a crude way) but should be 
    /// relatively easy to implement as they are similar to MS Access and 
    /// Sql Server Data access objects.
    ///
    /// Enterprise Design Pattern: Service Stub.
    /// </summary>
    public class OracleOrderDao : IOrderDao
    {
        /// <summary>
        /// Gets customers with order statistics in given sort order. Stubbed.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <returns>Sorted list of customers with order statistics.</returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a list of customers with order summary statistics. Stubbed.
        /// </summary>
        /// <param name="customers">Customer list.</param>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Customer list with order summary statistics.</returns>
        public IList<Customer> GetOrderStatistics(IList<Customer> customers, string sortExpression)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets all orders for a customer. Stubbed.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>List of orders.</returns>
        public IList<Order> GetOrders(int customerId)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a list of orders placed within a date range. Stubbed.
        /// </summary>
        /// <param name="dateFrom">Date range begin date.</param>
        /// <param name="dateThru">Date range end date.</param>
        /// <returns>List of orders.</returns>
        public IList<Order> GetOrdersByDate(DateTime dateFrom, DateTime dateThru)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a list of order details for a given order. Stubbed.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>List of order details.</returns>
        public IList<OrderDetail> GetOrderDetails(int orderId)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a list of orders. Private helper method. Stubbed.
        /// </summary>
        /// <param name="sql">Sql statement.</param>
        /// <returns>List of orders.</returns>
        private IList<Order> GetOrderList(string sql)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets an order. Stubbed.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>Order.</returns>
        public Order GetOrder(int orderId)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }
    }
}
