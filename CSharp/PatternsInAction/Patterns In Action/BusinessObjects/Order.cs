using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessObjects
{
    /// <summary>
    /// Class that holds information about an order.
    /// </summary>
    /// <remarks>
    /// Enterprise Design Pattern: Domain Model, Identity Field, Foreign Key Mapping.
    /// 
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// 
    /// The Identity Field Design Pattern saves the ID field in an object to maintain
    /// identity between an in-memory business object and that database rows.
    /// 
    /// The Foreign Key Mapping Design Pattern is implemented by the Order to Customer 
    /// reference. The pattern states that it maps an association between objects to 
    /// a foreign key reference between table. The CustomerId is the foreign key to the 
    /// Order. 
    /// </remarks>
    public class Order : BusinessObject
    {
        /// <summary>
        /// Default constructor for order class.
        /// </summary>
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
            Version = _versionDefault;
        }

        /// <summary>
        /// Overloaded constructor for the order class.
        /// </summary>
        /// <param name="orderId">Unique identifier for the Order</param>
        /// <param name="orderDate">Date at which Order is placed.</param>
        /// <param name="requiredDate">Date at which Order is required.</param>
        /// <param name="freight">Freight (shipping) costs for the Order.</param>
        public Order(int orderId, DateTime orderDate, DateTime requiredDate, float freight)
            : this()
        {
            OrderId = orderId;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            Freight = freight;
        }

        /// <summary>
        /// Gets or sets unique identifier for the order.
        /// Enterprise Design Pattern: Identity field pattern.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the date at which the order is placed.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Gets or sets the date at which delivery of the order is required.
        /// </summary>
        public DateTime RequiredDate { get; set; }

        /// <summary>
        /// Gets or sets the freight (shipping) costs for this order.
        /// </summary>
        public float Freight { get; set; }

        /// <summary>
        /// Gets or sets the customer associated with the order.
        /// Enterprise Design Pattern: Foreign Key Mapping. Customer is the parent.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the customer name associated with the order.
        /// </summary>
        /// public string CustomerName{ get; set; }

        /// <summary>
        /// Gets or sets a list of order details (line items) for the order.
        /// </summary>
        public IList<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets version. Used in support of optimistic concurrency.
        /// </summary>
        public string Version { get; set; }
    }
}
