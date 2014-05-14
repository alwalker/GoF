using System;
using System.Data;
using System.Configuration;
using System.Xml.Serialization;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using System.Runtime.Serialization;

namespace ActionService.DataTransferObjects
{
    /// <summary>
    /// Product Data Transfer Object.
    /// 
    /// The purpose of the CustomerTransferObject is to facilitate transport of 
    /// customer business data in a serializable format. Business data is kept in 
    /// publicly accessible auto property members. This class has no methods. 
    /// </summary>
    /// <remarks>
    /// Pattern: Data Transfer Objects.
    /// 
    /// Data Transfer Objects are objects that transfer data between processes, but without behavior.
    /// </remarks>
    [DataContract(Name = "Product", Namespace = "http://www.yourcompany.com/types/")]
    public class ProductDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// The Identity Field Design Pattern. 
        /// </summary>
        [DataMember]
        public int ProductId { get; set; }

        /// <summary>
        /// Getd or sets the product name.
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the weight of the product.
        /// </summary>
        [DataMember]
        public string Weight { get; set; }

        /// <summary>
        /// Gets or sets the unit price of the product in US$.
        /// </summary>
        [DataMember]
        public double UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets units in stock for the product.
        /// </summary>
        [DataMember]
        public int UnitsInStock { get; set; }

        /// <summary>
        /// Gets category name under which product is categorized.
        /// </summary>
        [DataMember]
        public CategoryDto Category { get; set; }

        /// <summary>
        /// Gets or sets version. Used in support of optimistic concurrency.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}

