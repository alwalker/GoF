using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

using System.Collections.Generic;
using BusinessObjects;
using System.Runtime.Serialization;

using ActionService.MessageBase;
using ActionService.DataTransferObjects;

namespace ActionService.Messages
{
    /// <summary>
    /// Represents a customer response message to client
    /// </summary>    
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class CustomerResponse : ResponseBase
    {
        /// <summary>
        /// List of customers. 
        /// </summary>
        [DataMember]
        public IList<CustomerDto> Customers;

        /// <summary>
        /// Single customer
        /// </summary>
        [DataMember]
        public CustomerDto Customer;
    }
}
