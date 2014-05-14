using System;
using System.Data;
using System.Configuration;
using System.Runtime.Serialization;

using ActionService.MessageBase;
using ActionService.Criteria;
using ActionService.DataTransferObjects;

namespace ActionService.Messages
{
    /// <summary>
    /// Represents a customer request message from client.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class CustomerRequest : RequestBase
    {
        /// <summary>
        /// Selection criteria and sort order
        /// </summary>
        [DataMember]
        public CustomerCriteria Criteria;

        [DataMember]
        public CustomerDto Customer;


    }
}
