using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization;

using ActionService.MessageBase;
using ActionService.Criteria;

namespace ActionService.Messages
{
    /// <summary>
    /// Respresents a product request message from client to web service.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class ProductRequest : RequestBase
    {
        /// <summary>
        /// Selection criteria and sort order
        /// </summary>
        [DataMember]
        public ProductCriteria Criteria;
    }
}