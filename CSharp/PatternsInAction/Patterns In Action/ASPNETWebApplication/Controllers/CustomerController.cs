using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;

using ASPNETWebApplication.ActionServiceReference;

namespace ASPNETWebApplication.Controllers
{
    /// <summary>
    /// Controller for customer related objects.
    /// </summary>
    /// <remarks>
    /// MV Patterns: Model View Controller Pattern.
    /// This is a 'loose' implementation of the MVC pattern.
    /// </remarks>
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// Gets a list of customers.
        /// </summary>
        /// <param name="sortExpression">Desired sort order of the customer list.</param>
        /// <returns>List of customers.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<Customer> GetCustomers(string sortExpression)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Customers" };
            request.Criteria = new CustomerCriteria { SortExpression = sortExpression };

            CustomerResponse response = ActionServiceClient.GetCustomers(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCustomers: RequestId and CorrelationId do not match.");

            return response.Customers;
        }

        /// <summary>
        /// Gets list of customers, each of which contains order statistics.
        /// </summary>
        /// <param name="sortExpression">Sortorder for returned customer list.</param>
        /// <returns>Sorted customer list with order stats.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public IList<Customer> GetCustomersWithOrderStatistics(string sortExpression)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Customers" };
            request.Criteria = new CustomerCriteria { SortExpression = sortExpression, IncludeOrderStatistics = true };

            CustomerResponse response = ActionServiceClient.GetCustomers(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCustomers: RequestId and CorrelationId do not match.");

            return response.Customers;
        }

        /// <summary>
        /// Gets a specific customer record.
        /// </summary>
        /// <param name="customerId">Unique customer object.</param>
        /// <returns>Customer object.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Customer GetCustomer(int customerId)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Customer" };
            request.Criteria = new CustomerCriteria { CustomerId = customerId };

            CustomerResponse response = ActionServiceClient.GetCustomers(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCustomers: RequestId and CorrelationId do not match.");

            return response.Customer;
        }

        /// <summary>
        /// Get customer object with all its orders.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Customer object.</returns>
        public Customer GetCustomerWithOrders(int customerId)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Customer", "Orders" };
            request.Criteria = new CustomerCriteria { CustomerId = customerId };

            CustomerResponse response = ActionServiceClient.GetCustomers(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCustomersWithOrders: RequestId and CorrelationId do not match.");

            return response.Customer;
        }

        /// <summary>
        /// Adds a new customer to the database
        /// </summary>
        /// <param name="customer">Customer object to be added.</param>
        [DataObjectMethod(DataObjectMethodType.Insert)]
        public void AddCustomer(Customer customer)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Create";
            request.Customer = customer;

            CustomerResponse response = ActionServiceClient.SetCustomers(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("AddCustomer: RequestId and CorrelationId do not match.");

            // These messages are for public consumption. Includes validation errors.
            if (response.Acknowledge == AcknowledgeType.Failure)
                throw new ApplicationException(response.Message);
        }

        /// <summary>
        /// Updates a customer in the database.
        /// </summary>
        /// <param name="customer">Customer object with updated values.</param>
        [DataObjectMethod(DataObjectMethodType.Update)]
        public void UpdateCustomer(Customer customer)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Update";
            request.Customer = customer;

            CustomerResponse response = ActionServiceClient.SetCustomers(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("UpdateCustomer: RequestId and CorrelationId do not match.");

            // These messages are for public consumption. Includes validation errors.
            if (response.Acknowledge == AcknowledgeType.Failure)
                throw new ApplicationException(response.Message);
        }

        /// <summary>
        /// Deletes a customer from the database. A customer can only be deleted if no orders were placed.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Number of orders deleted.</returns>
        [DataObjectMethod(DataObjectMethodType.Delete)]
        public int DeleteCustomer(int customerId)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Delete";
            request.Criteria = new CustomerCriteria { CustomerId = customerId };

            CustomerResponse response = ActionServiceClient.SetCustomers(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("DeleteCustomer: RequestId and CorrelationId do not match.");

            if (response.Acknowledge == AcknowledgeType.Failure)
                throw new ApplicationException(response.Message);

            return response.RowsAffected;
        }
    }
}