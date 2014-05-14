using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using BusinessObjects;

namespace DataObjects.AdoNet.Oracle
{
    /// <summary>
    /// Oracle specific data access object that handles data access
    /// of customers. The details are stubbed out (in a crude way) but should be 
    /// relatively easy to implement as they are similar to MS Access and 
    /// Sql Server Data access objects.
    ///
    /// Enterprise Design Pattern: Service Stub.
    /// </summary>
    public class OracleCustomerDao : ICustomerDao
    {
        /// <summary>
        /// Gets a list of all customers.
        /// </summary>
        /// <returns>Customer list.</returns>
        public IList<Customer> GetCustomers()
        {
            string sortExpression = null;
            return GetCustomers(sortExpression);
        }

        /// <summary>
        /// Gets a sorted list of all customers. Stubbed.
        /// </summary>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of customers.</returns>
        public IList<Customer> GetCustomers(string sortExpression)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets a customer. Stubbed.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Customer.</returns>
        public Customer GetCustomer(int customerId)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Gets customer given an order. Stubbed.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>Customer.</returns>
        public Customer GetCustomerByOrder(int orderId)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Inserts a new customer. Stubbed.
        /// </summary>
        /// <remarks>
        /// Following insert, customer object will contain the new identifier.
        /// </remarks>
        /// <param name="customer">Customer.</param>
        public void InsertCustomer(Customer customer)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Updates a customer. Stubbed.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of customer records updated.</returns>
        public int UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }

        /// <summary>
        /// Deletes a customer. Stubbed.
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns>Number of customer records deleted.</returns>
        public int DeleteCustomer(Customer customer)
        {
            throw new NotImplementedException("Oracle data access not implemented.");
        }
    }
}
