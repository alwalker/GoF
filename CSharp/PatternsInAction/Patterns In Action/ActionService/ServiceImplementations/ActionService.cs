using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

using System.Web.Security;
using System.Collections.Generic;

using System.ServiceModel;
using System.ServiceModel.Activation;

using Cart;
using Transactions;
using Encryption;
using BusinessObjects;
using DataObjects;

using ActionService.Criteria;
using ActionService.DataTransferObjects;
using ActionService.Messages;
using ActionService.MessageBase;
using ActionService.DataTransferObjectMapper;
using ActionService.ServiceContracts;

namespace ActionService.ServiceImplementations
{
    /// <summary>
    /// Main facade into Patterns in Action application
    /// </summary>
    /// <remarks>
    /// The Cloud Facade Pattern.
    /// </remarks>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class ActionService : IActionService
    {
        private static string methodName = ConfigurationManager.AppSettings.Get("ShippingMethod");
        private static ShippingMethod defaultShippingMethod = (ShippingMethod)Enum.Parse(typeof(ShippingMethod), methodName);

        // Create static data access objects
        private static IProductDao productDao = DataAccess.ProductDao;
        private static ICustomerDao customerDao = DataAccess.CustomerDao;
        private static IOrderDao orderDao = DataAccess.OrderDao;

        private string _accessToken;
        private ShoppingCart _shoppingCart; 
        private string _userName;

        /// <summary>
        /// Gets unique session based token that is valid for the duration of the session.
        /// </summary>
        /// <param name="request">Token request message.</param>
        /// <returns>Token response message.</returns>
        public TokenResponse GetToken(TokenRequest request)
        {
            TokenResponse response = new TokenResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag only
            if (!ValidRequest(request, response, Validate.ClientTag))
                return response;

            // Note: these are session based and expire when session expires.
            _accessToken = Guid.NewGuid().ToString();
            _shoppingCart = new ShoppingCart(defaultShippingMethod);

            response.AccessToken = _accessToken;
            return response;
        }

        /// <summary>
        /// Login to application service.
        /// </summary>
        /// <param name="request">Login request message.</param>
        /// <returns>Login response message.</returns>
        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse response = new LoginResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag and access token
            if (!ValidRequest(request, response, Validate.ClientTag | Validate.AccessToken))
                return response;

            if (!Membership.ValidateUser(request.UserName, request.Password))
            {
                response.Acknowledge = AcknowledgeType.Failure;
                response.Message = "Invalid username and/or password.";
                return response;
            }
           
            _userName = request.UserName;

            return response;
        }

        /// <summary>
        /// Logout from application service.
        /// </summary>
        /// <param name="request">Logout request message.</param>
        /// <returns>Login request message.</returns>
        public LogoutResponse Logout(LogoutRequest request)
        {
            LogoutResponse response = new LogoutResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag and access token
            if (!ValidRequest(request, response, Validate.ClientTag | Validate.AccessToken))
                return response;

            _userName = null;

            return response;
        }

        /// <summary>
        /// Request customer data.
        /// </summary>
        /// <param name="request">Customer request message.</param>
        /// <returns>Customer response message.</returns>
        public CustomerResponse GetCustomers(CustomerRequest request)
        {
            CustomerResponse response = new CustomerResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag, access token, and user credentials
            if (!ValidRequest(request, response, Validate.All))
                return response;

            CustomerCriteria criteria = request.Criteria as CustomerCriteria;
            string sort = criteria.SortExpression;

            if (request.LoadOptions.Contains("Customers"))
            {
                IEnumerable<Customer> customers;
                if (!criteria.IncludeOrderStatistics)
                {
                    // Simple customer list without order information
                    customers = customerDao.GetCustomers(criteria.SortExpression);
                }
                else if (sort.IndexOf("NumOrders") >= 0 || sort.IndexOf("LastOrderDate") >= 0)
                {
                    // Sort order is handled by the Order Dao
                    IList<Customer> list = customerDao.GetCustomers();
                    customers = orderDao.GetOrderStatistics(list, sort);
                }
                else
                {
                    // Sort order is handled by the Customer Dao, but alse need order statistics
                    IList<Customer> list = customerDao.GetCustomers(sort);
                    customers = orderDao.GetOrderStatistics(list);
                }
                response.Customers = customers.Select(c => Mapper.ToDataTransferObject(c)).ToList();
            }

            if (request.LoadOptions.Contains("Customer"))
            {
                Customer customer = customerDao.GetCustomer(criteria.CustomerId);
                if (request.LoadOptions.Contains("Orders"))
                    customer.Orders = orderDao.GetOrders(customer.CustomerId);

                response.Customer = Mapper.ToDataTransferObject(customer);
            }

            return response;
        }

        /// <summary>
        /// Set (add, update, delete) customer value.
        /// </summary>
        /// <param name="request">Customer request message.</param>
        /// <returns>Customer response message.</returns>
        public CustomerResponse SetCustomers(CustomerRequest request)
        {
            CustomerResponse response = new CustomerResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag, access token, and user credentials
            if (!ValidRequest(request, response, Validate.All))
                return response;

            // Transform customer data transfer object to customer business object
            Customer customer = Mapper.FromDataTransferObject(request.Customer);

            // Validate customer business rules

            if (request.Action != "Delete")
            {
                if (!customer.Validate())
                {
                    response.Acknowledge = AcknowledgeType.Failure;

                    foreach (string error in customer.ValidationErrors)
                        response.Message += error + Environment.NewLine;

                    return response;
                }
            }

            // Run within the context of a database transaction. Currently commented out.
            // The Decorator Design Pattern. 
            //using (TransactionDecorator transaction = new TransactionDecorator())
            {
                if (request.Action == "Create")
                {
                    customerDao.InsertCustomer(customer);
                    response.Customer = Mapper.ToDataTransferObject(customer);
                    response.RowsAffected = 1;
                }
                else if (request.Action == "Update")
                {
                    response.RowsAffected = customerDao.UpdateCustomer(customer);
                    response.Customer = Mapper.ToDataTransferObject(customer);
                }
                else if (request.Action == "Delete")
                {
                    CustomerCriteria criteria = request.Criteria as CustomerCriteria;
                    Customer cust = customerDao.GetCustomer(criteria.CustomerId);

                    response.RowsAffected = customerDao.DeleteCustomer(cust);
                }

                //transaction.Complete();
            }

            return response;
        }

        /// <summary>
        /// Request order data.
        /// </summary>
        /// <param name="request">Order request message.</param>
        /// <returns>Order response message.</returns>
        public OrderResponse GetOrders(OrderRequest request)
        {
            OrderResponse response = new OrderResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag, access token, and user credentials
            if (!ValidRequest(request, response, Validate.All))
                return response;


            OrderCriteria criteria = request.Criteria as OrderCriteria;

            if (request.LoadOptions.Contains("Order"))
            {
                Order order = orderDao.GetOrder(criteria.OrderId);

                if (request.LoadOptions.Contains("Customer"))
                    order.Customer = customerDao.GetCustomerByOrder(order.OrderId);

                if (request.LoadOptions.Contains("OrderDetails"))
                    order.OrderDetails = orderDao.GetOrderDetails(order.OrderId);

                response.Order = Mapper.ToDataTransferObject(order);
            }

            if (request.LoadOptions.Contains("Orders"))
            {
                Customer customer = customerDao.GetCustomer(criteria.CustomerId);

                IList<Order> orders = orderDao.GetOrders(customer.CustomerId);
                if (request.LoadOptions.Contains("OrderDetails"))
                {
                    foreach (Order order in orders)
                        order.OrderDetails = orderDao.GetOrderDetails(order.OrderId);
                }

                response.Orders = Mapper.ToDataTransferObjects(orders);
            }

            return response;
        }

        // Not implemented. No orders are taken.
        public OrderResponse SetOrders(OrderRequest request)
        {
            return new OrderResponse();
        }

        /// <summary>
        /// Requests product data.
        /// </summary>
        /// <param name="request">Product request message.</param>
        /// <returns>Product response message.</returns>
        public ProductResponse GetProducts(ProductRequest request)
        {
            ProductResponse response = new ProductResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag and access token
            if (!ValidRequest(request, response, Validate.ClientTag | Validate.AccessToken))
                return response;


            ProductCriteria criteria = request.Criteria as ProductCriteria;

            if (request.LoadOptions.Contains("Categories"))
            {
                IEnumerable<Category> categories = productDao.GetCategories();
                response.Categories = Mapper.ToDataTransferObjects(categories);
            }

            if (request.LoadOptions.Contains("Products"))
            {
                IEnumerable<Product> products = productDao.GetProductsByCategory(criteria.CategoryId, criteria.SortExpression);
                response.Products = Mapper.ToDataTransferObjects(products);
            }

            if (request.LoadOptions.Contains("Product"))
            {
                Product product = productDao.GetProduct(criteria.ProductId);
                product.Category = productDao.GetCategoryByProduct(criteria.ProductId);

                response.Product = Mapper.ToDataTransferObject(product);
            }

            if (request.LoadOptions.Contains("Search"))
            {
                IList<Product> products = productDao.SearchProducts(criteria.ProductName,
                    criteria.PriceFrom, criteria.PriceThru, criteria.SortExpression);

                response.Products = Mapper.ToDataTransferObjects(products);
            }
            return response;
        }

        // Not implemented. No products are modified.
        public ProductResponse SetProducts(ProductRequest request)
        {
            return new ProductResponse();
        }

        /// <summary>
        /// Request shopping cart.
        /// </summary>
        /// <param name="request">Shopping cart request message.</param>
        /// <returns>Shopping cart response message.</returns>
        public CartResponse GetCart(CartRequest request)
        {
            CartResponse response = new CartResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag and access token
            if (!ValidRequest(request, response, Validate.ClientTag | Validate.AccessToken))
                return response;

            // Always return recomputed shopping cart
            response.Cart = Mapper.ToDataTransferObject(_shoppingCart);

            return response;
        }

        /// <summary>
        /// Sets (add, edit, delete) shopping cart data.
        /// </summary>
        /// <param name="request">Shopping cart request message.</param>
        /// <returns>Shopping cart response message.</returns>
        public CartResponse SetCart(CartRequest request)
        {
            CartResponse response = new CartResponse();
            response.CorrelationId = request.RequestId;

            // Validate client tag and access token
            if (!ValidRequest(request, response, Validate.ClientTag | Validate.AccessToken))
                return response;

            if (request.Action == "Read")
            {
                // Do nothing, just return cart    
            }
            else if (request.Action == "Create")
            {
                _shoppingCart.AddItem(request.CartItem.Id, request.CartItem.Name,
                    request.CartItem.Quantity, request.CartItem.UnitPrice);
            }
            else if (request.Action == "Update")
            {
                // Either shipping method or quantity requires update
                if (!string.IsNullOrEmpty(request.ShippingMethod))
                    _shoppingCart.ShippingMethod = (ShippingMethod)Enum.Parse(typeof(ShippingMethod), request.ShippingMethod);
                else
                    _shoppingCart.UpdateQuantity(request.CartItem.Id, request.CartItem.Quantity);
            }
            else if (request.Action == "Delete")
            {
                _shoppingCart.RemoveItem(request.CartItem.Id);
            }

            _shoppingCart.ReCalculate();
            response.Cart = Mapper.ToDataTransferObject(_shoppingCart);

            return response;
        }

        /// <summary>
        /// Validate 3 security levels for a request: ClientTag, AccessToken, and User Credentials
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <param name="response">The response message.</param>
        /// <param name="validate">The validation that needs to take place.</param>
        /// <returns></returns>
        private bool ValidRequest(RequestBase request, ResponseBase response, Validate validate)
        {
            // Validate Client Tag. In production this should query a 'client' table in a database.
            if ((Validate.ClientTag & validate) == Validate.ClientTag)
            {
                if (request.ClientTag != "ABC123")
                {
                    response.Acknowledge = AcknowledgeType.Failure;
                    response.Message = "Unknown Client Tag";
                    return false;
                }
            }

            // Validate access token
            if ((Validate.AccessToken & validate) == Validate.AccessToken)
            {
                if (_accessToken == null) 
                {
                    response.Acknowledge = AcknowledgeType.Failure;
                    response.Message = "Invalid or expired AccessToken. Call GetToken()";
                    return false;
                }
            }

            // Validate user credentials
            if ((Validate.UserCredentials & validate) == Validate.UserCredentials)
            {
                if (_userName == null) 
                {
                    response.Acknowledge = AcknowledgeType.Failure;
                    response.Message = "Please login and provide user credentials before accessing these methods.";
                    return false;
                }
            }


            return true;
        }

        #region Used in primary key encryption. Not currently used.

        /// <summary>
        /// Encrypts internal identifier before sending it out to client.
        /// Private helper method.
        /// </summary>
        /// <param name="id">Identifier to be encrypted.</param>
        /// <param name="tableName">Database table in which identifier resides.</param>
        /// <returns>Encrypted stringified identifier.</returns>
        private string EncryptId(int id, string tableName)
        {
            string s = id.ToString() + "|" + tableName;
            return Crypto.ActionEncrypt(s);
        }

        /// <summary>
        /// Decrypts identifiers that come back from client.
        /// Private helper method.
        /// </summary>
        /// <param name="sid">Stringified, encrypted identifier.</param>
        /// <returns>Internal identifier.</returns>
        private int DecryptId(string sid)
        {
            string s = Crypto.ActionDecrypt(sid);
            s = s.Substring(0, s.IndexOf("|"));
            return int.Parse(s);
        }

        #endregion

        /// <summary>
        /// Validation options enum. Used in validation of messages.
        /// </summary>
        [Flags]
        private enum Validate
        {
            ClientTag = 0x0001,
            AccessToken = 0x0002,
            UserCredentials = 0x0004,
            All = ClientTag | AccessToken | UserCredentials
        }
    }
}
