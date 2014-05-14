
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
    /// Order Detail Data Transfer Object.
    /// 
    /// The purpose of the OrderTransferObject is to facilitate transport of 
    /// customer order business data in a serializable format. Business data is kept in 
    /// publicly accessible auto property members. This class has no methods. 
    /// </summary>
    /// <remarks>
    /// Pattern: Data Transfer Objects.
    /// 
    /// Data Transfer Objects are objects that transfer data between processes, but without behavior.
    /// </remarks>
    [DataContract(Name = "OrderDetail", Namespace = "http://www.yourcompany.com/types/")]
    public class OrderDetailDto
    {
        /// <summary>
        /// Product name ordered.
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

        /// <summary>
        /// Quantity ordered.
        /// </summary>
        [DataMember]
        public int Quantity { get; set; }

        /// <summary>
        /// Unit price for product at order time.
        /// </summary>
        [DataMember]
        public float UnitPrice { get; set; }

        /// <summary>
        /// Discount applied to unit price. 
        /// </summary>
        [DataMember]
        public float Discount { get; set; }

        /// <summary>
        /// Version number. Used in optimistic concurrency.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}