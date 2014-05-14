using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using WindowsFormsModel.DataTransferObjectMapper;
using WindowsFormsModel.BusinessObjects;
using WindowsFormsModel.ActionServiceReference;

using WCFProxy;

namespace WindowsFormsModel
{
    /// <summary>
    /// The Model in MVP design pattern. 
    /// Implements IModel and communicates with WCF Service.
    /// </summary>
    public class Model : IModel
    {
        #region statics

        private static ActionServiceClient Service { get; set; }
        private static string AccessToken { get; set; }
        private static string ClientTag { get; set; }

        /// <summary>
        /// Static constructor
        /// </summary>
        static Model()
        {
            Service = new ActionServiceClient();

            // Gets client tag from app.config configuration file
            ClientTag = ConfigurationManager.AppSettings.Get("ClientTag");

            // Retrieve AccessToken as first step
            TokenRequest request = new TokenRequest();
            request.RequestId = NewRequestId;
            request.ClientTag = ClientTag;

            TokenResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.GetToken(request); });

            // Store access token for subsequent service calls.
            AccessToken = response.AccessToken;
        }

        /// <summary>
        /// Gets a unique request id.
        /// </summary>
        private static string NewRequestId
        {
            get { return Guid.NewGuid().ToString(); }
        }

        #endregion

        #region Login / Logout

        /// <summary>
        /// Logs in to the service.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        public void Login(string userName, string password)
        {
            LoginRequest request = new LoginRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.UserName = userName;
            request.Password = password;

            LoginResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.Login(request); });
            
            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("Login: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);
        }

        /// <summary>
        /// Logs out of the service.
        /// </summary>
        public void Logout()
        {
            LogoutRequest request = new LogoutRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            LogoutResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.Logout(request); });

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("Logout: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);
        }

        #endregion

        #region Customers 

        /// <summary>
        /// Gets a list of all customers in the given sort order.
        /// </summary>
        /// <param name="sortExpression">Sort order.</param>
        /// <returns>List of customers.</returns>
        public IList<CustomerModel> GetCustomers(string sortExpression)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Customers" };
            request.Criteria = new CustomerCriteria { SortExpression = sortExpression };

            CustomerResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.GetCustomers(request); });

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCustomers: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);

            return Mapper.FromDataTransferObjects(response.Customers);
        }

        /// <summary>
        /// Gets a specific customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Customer.</returns>
        public CustomerModel GetCustomer(int customerId)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Customer" };
            request.Criteria = new CustomerCriteria { CustomerId = customerId };

            CustomerResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.GetCustomers(request); });

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCustomer: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);

            return Mapper.FromDataTransferObject(response.Customer);
        }

        #endregion

        #region Customer persistence

        /// <summary>
        /// Adds a new customer to the database.
        /// </summary>
        /// <param name="customer">Customer.</param>
        /// <returns>Number of records affected. If all worked well, then should be 1.</returns>
        public int AddCustomer(CustomerModel customer)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Create";
            request.Customer = Mapper.ToDataTransferObject(customer);

            CustomerResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.SetCustomers(request); });

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("AddCustomer: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);

            return response.RowsAffected;

        }

        /// <summary>
        /// Updates an existing customer in the database.
        /// </summary>
        /// <param name="customer">The updated customer.</param>
        /// <returns>Number or records affected. Should be 1.</returns>
        public int UpdateCustomer(CustomerModel customer)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Update";
            request.Customer = Mapper.ToDataTransferObject(customer);

            CustomerResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.SetCustomers(request); });

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("UpdateCustomer: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);

            return response.RowsAffected;
        }

        /// <summary>
        /// Deletes a customer record.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>Number of records affected. Should be 1.</returns>
        public int DeleteCustomer(int customerId)
        {
            CustomerRequest request = new CustomerRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Delete";
            request.Criteria = new CustomerCriteria { CustomerId = customerId };

            CustomerResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.SetCustomers(request); });

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("DeleteCustomer: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);

            return response.RowsAffected;
        }

        #endregion

        #region Orders

        /// <summary>
        /// Gets a list of orders for a given customer.
        /// </summary>
        /// <param name="customerId">Unique customer identifier.</param>
        /// <returns>List of orders.</returns>
        public IList<OrderModel> GetOrders(int customerId)
        {
            OrderRequest request = new OrderRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Orders", "OrderDetails", "Product" };
            request.Criteria = new OrderCriteria { CustomerId = customerId, SortExpression = "OrderId ASC" };

            OrderResponse response = null;
            SafeProxy.DoAction<ActionServiceClient>(Service, client =>
                { response = client.GetOrders(request); });

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetOrders: RequestId and CorrelationId do not match.");

            if (response.Acknowledge != AcknowledgeType.Success)
                throw new ApplicationException(response.Message);

            return Mapper.FromDataTransferObjects(response.Orders);
        }

        #endregion
    }
}
