using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BusinessObjects;

namespace DataObjects
{
    /// <summary>
    /// Defines methods to access and maintain customer data.
    /// This is a database-independent interface. 
    /// The implementations will be database specific.
    /// </summary>
    public interface ICustomerDao
    {
        /// <summary>
        /// Gets a list of all customers.
        /// </summary>
        /// <returns>List of customers.</returns>
        IList<Customer> GetCustomers();

        /// <summary>
        /// Gets a sorted list of all customers.
        /// </summary>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>Sorted list of customers.</returns>
        IList<Customer> GetCustomers(string sortExpression);

        /// <summary>
        /// Gets a specific customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Customer.</returns>
        Customer GetCustomer(int customerId);
        
        /// <summary>
        /// Gets customer given an order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>Customer.</returns>
        Customer GetCustomerByOrder(int orderId);

        /// <summary>
        /// Inserts a new customer. 
        /// </summary>
        /// <remarks>
        /// Following insert, customer object will contain the new identifier.
        /// </remarks>
        /// <param name="customer">Customer.</param>
        void InsertCustomer(Customer customer);

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of customer records updated.</returns>
        int UpdateCustomer(Customer customer);

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of customer records deleted.</returns>
        int DeleteCustomer(Customer customer);
    }
}
