using System;
using System.Data;
using System.Configuration;
using System.Runtime.Serialization;

using ActionService.Criteria;
using ActionService.MessageBase;
using ActionService.DataTransferObjects;

namespace ActionService.Messages
{
    /// <summary>
    /// Respresents a order request message from client to web service.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class OrderRequest : RequestBase
    {
        /// <summary>
        /// Selection criteria and sort order
        /// </summary>
        [DataMember]
        public OrderCriteria Criteria;

        [DataMember]
        public int OrderId;
    }
}
