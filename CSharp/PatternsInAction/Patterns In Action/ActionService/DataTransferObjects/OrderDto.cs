
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
    /// Order Data Transfer Object.
    /// 
    /// The purpose of the CustomerTransferObject is to facilitate transport of 
    /// customer business data in a serializable format. Business data is kept in 
    /// publicly accessible auto property members. This class has no methods. 
    /// </summary>
    /// <remarks>
    /// Pattern: Data Data Transfer Objects.
    /// 
    /// Data Transfer Objects are objects that transfer data between processes, but without behavior.
    /// </remarks>
    [DataContract(Name = "Order", Namespace = "http://www.yourcompany.com/types/")]
    public class OrderDto
    {
        /// <summary>
        /// Unique order identifier.
        /// The Identity Field Design Pattern. 
        /// </summary>
        [DataMember]
        public int OrderId { get; set; }

        /// <summary>
        /// Date the order is placed.
        /// </summary>
        [DataMember]
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Date the order is required for delivery.
        /// </summary>
        [DataMember]
        public DateTime RequiredDate { get; set; }

        /// <summary>
        /// Freight or shipping costs for the order.
        /// </summary>
        [DataMember]
        public float Freight { get; set; }

        /// <summary>
        /// List of order details (line items) for the order.
        /// </summary>
        [DataMember]
        public OrderDetailDto[] OrderDetails { get; set; }


        /// <summary>
        /// Not used yet
        /// </summary>
        public CustomerDto Customer { get; set; }

        /// <summary>
        /// Version number. Used in optimistic concurrency.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}