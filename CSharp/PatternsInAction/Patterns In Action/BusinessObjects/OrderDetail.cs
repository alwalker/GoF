using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessObjects
{
    /// <summary>
    /// Class that holds order details (line items) for an order.
    /// </summary>
    /// <remarks>
    /// Enterprise Design Pattern: Domain Model.
    /// 
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic and business rules is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// </remarks>
    public class OrderDetail : BusinessObject
    {
        /// <summary>
        /// Default constructor for Order Detail.
        /// </summary>
        public OrderDetail()
        {
            Version = _versionDefault;
        }

        /// <summary>
        /// Overloaded  constructor for Order Detail.
        /// </summary>
        /// <param name="productName">Product name of Order Detail.</param>
        /// <param name="quantity">Quantity ordered.</param>
        /// <param name="unitPrice">Unit price of product at the time order is placed.</param>
        /// <param name="discount">Discount applied to unit price of product.</param>
        /// <param name="order">Order that Order Detail is part of.</param>
        public OrderDetail(string productName, int quantity, float unitPrice, float discount, Order order)
            : this()
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            Order = order; // The parent object
        }

        /// <summary>
        /// Get or set Product name of Order Detail (line item).
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Get or set quantity of Products ordered.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Get or set unit price of Product in US$.
        /// </summary>
        public float UnitPrice { get; set; }

        /// <summary>
        /// Get or set discount applied to unit price.
        /// </summary>
        public float Discount { get; set; }

        /// <summary>
        /// Get or set the Order of which this Order Detail is part of.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets version. Used in support of optimistic concurrency.
        /// </summary>
        public string Version { get; set; }
    }
}
