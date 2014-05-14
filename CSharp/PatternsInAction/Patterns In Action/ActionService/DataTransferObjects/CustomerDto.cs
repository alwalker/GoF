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
    /// Customer Data Transfer Object.
    /// 
    /// The purpose of the CustomerTransferObject is to facilitate transport of 
    /// customer business data in a serializable format. Business data is kept in 
    /// publicly accessible auto properties. This class has no methods. 
    /// </summary>
    /// <remarks>
    /// Pattern: Data Transfer Objects.
    /// 
    /// Data Transfer Objects are objects that transfer data between processes, but without behavior.
    /// </remarks>
    [DataContract(Name = "Customer", Namespace = "http://www.yourcompany.com/types/")]
    public class CustomerDto
    {
        /// <summary>
        /// Unique customer identifier.
        /// The Identity Field Design Pattern. 
        /// </summary>
        [DataMember]
        public int CustomerId { get; set; }

        /// <summary>
        /// Customer or company name.
        /// </summary>
        [DataMember]
        public string Company { get; set; }

        /// <summary>
        /// Customer city.
        /// </summary>
        [DataMember]
        public string City { get; set; }

        /// <summary>
        /// Customer country.
        /// </summary>
        [DataMember]
        public string Country { get; set; }

        /// <summary>
        /// Total number of orders placed by customer.
        /// </summary>
        [DataMember]
        public int NumOrders { get; set; }

        /// <summary>
        /// Last order date for customer.
        /// </summary>
        [DataMember]
        public DateTime LastOrderDate { get; set; }

        /// <summary>
        /// List of orders placed by customer.
        /// </summary>
        [DataMember]
        public OrderDto[] Orders { get; set; }

        /// <summary>
        /// Version number. Used in optimistic concurrency.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}