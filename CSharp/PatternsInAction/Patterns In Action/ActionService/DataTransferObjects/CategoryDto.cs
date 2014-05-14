using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;

using System.Runtime.Serialization;

namespace ActionService.DataTransferObjects
{
    /// <summary>
    /// Category Data Transfer Object.
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
    [DataContract(Name = "Category", Namespace = "http://www.yourcompany.com/types/")]
    public class CategoryDto
    {
        /// <summary>
        /// Unique category identifier.
        /// The Identity Field Design Pattern. 
        /// </summary>
        [DataMember]
        public int CategoryId { get; set; }

        /// <summary>
        /// The category name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The category description.
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// Version number. Used in optimistic concurrency decisions.
        /// </summary>
        [DataMember]
        public string Version { get; set; }
    }
}
