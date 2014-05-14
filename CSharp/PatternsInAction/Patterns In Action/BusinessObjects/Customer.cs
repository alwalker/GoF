using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using BusinessObjects.BusinessRules;

namespace BusinessObjects
{
    /// <summary>
    /// Class that holds information about a customer.
    /// </summary>
    /// <remarks>
    /// Enterprise Design Pattern: Domain Model, Identity Field.
    /// 
    /// This is also the place where business rules are established.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// 
    /// The Identity Field Design Pattern saves the ID field in an object to maintain
    /// identity between an in-memory business object and that database rows.
    /// </remarks>
    public class Customer : BusinessObject
    {
        /// <summary>
        /// Default constructor for customer class.
        /// Initializes automatic properties.
        /// </summary>
        public Customer()
        {
            // Default property values
            Orders = new List<Order>();

            // Business rules
            AddRule(new ValidateId("CustomerId"));

            AddRule(new ValidateRequired("Company"));
            AddRule(new ValidateLength("Company", 1, 40));

            AddRule(new ValidateRequired("City"));
            AddRule(new ValidateLength("City", 1, 15));

            AddRule(new ValidateRequired("Country"));
            AddRule(new ValidateLength("Country", 1, 15));

            Version = _versionDefault;
        }

        /// <summary>
        /// Overloaded constructor for the Customer class.
        /// </summary>
        /// <param name="customerId">Unique Identifier for the Customer.</param>
        /// <param name="company">Name of the Customer.</param>
        /// <param name="city">City where Customer is located.</param>
        /// <param name="country">Country where Customer is located.</param>
        public Customer(int customerId, string company, string city, string country)
            : this()
        {
            CustomerId = customerId;
            Company = company;
            City = city;
            Country = country;
        }

        /// <summary>
        /// Gets or sets unique customer identifier.
        /// The Identity Field Design Pattern. 
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer name.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the customer city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the customer country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the total number of orders placed by the customer.
        /// </summary>
        public int NumOrders { get; set; }

        /// <summary>
        /// Gets or sets the last date the customer placed an order.
        /// </summary>
        public DateTime LastOrderDate { get; set; }

        /// <summary>
        /// Gets or sets a list of all orders placed by the customer.
        /// </summary>
        public IList<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the version number. Used for optimistic concurrency.
        /// </summary>
        public string Version { get; set; }
    }
}
