using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BusinessObjects
{
    /// <summary>
    /// Class that holds product information.
    /// </summary>
    /// <remarks>
    /// Enterprise Design Pattern: Domain Model, Identity Field.
    /// 
    /// This is where your business logic resides. In this example there are none.
    /// Another place for business logic and business rules is in the Facade.  
    /// For an example see CustomerFacade in the Facade layer.
    /// 
    /// The Domain Model Design Pattern states that domain objects incorporate 
    /// both behavior and data. Behavior may include simple or complex business logic.
    /// 
    /// The Identity Field Design Pattern saves the ID field in an object to maintain
    /// identity between an in-memory business object and that database rows.
    /// </remarks>
    public class Product : BusinessObject
    {
        /// <summary>
        /// Default constructor for product.
        /// </summary>
        public Product()
        {
            Version = _versionDefault;
        }

        /// <summary>
        /// Overloaded constructor for product
        /// </summary>
        /// <param name="productId">Unique identifier for Product</param>
        /// <param name="productName">Name of Product.</param>
        /// <param name="weight">Weight of Product.</param>
        /// <param name="unitPrice">Unit price of Product in US$.</param>
        /// <param name="unitsInStock">Product units in stock.</param>
        public Product(int productId, string productName, string weight, double unitPrice, int unitsInStock)
            : this()
        {
            ProductId = productId;
            ProductName = productName;
            Weight = weight;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// The Identity Field Design Pattern. 
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the weight of the product.
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product in US$.
        /// </summary>
        public double UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets units in stock for the product.
        /// </summary>
        public int UnitsInStock { get; set; }

        /// <summary>
        /// Gets or sets the product category under which product is categorized.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets category name under which product is categorized.
        /// </summary>
        ///public string CategoryName{ get; set; }

        /// <summary>
        /// Gets or sets version. Used in support of optimistic concurrency.
        /// </summary>
        public string Version { get; set; }
    }
}
