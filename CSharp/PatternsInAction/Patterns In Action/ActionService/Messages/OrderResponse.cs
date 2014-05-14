using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.Serialization;

using ActionService.MessageBase;
using ActionService.DataTransferObjects;

namespace ActionService.Messages
{
    /// <summary>
    /// Respresents a response message with a list of orders for a given customer.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class OrderResponse : ResponseBase
    {
        /// <summary>
        /// List of orders for a given customer.
        /// </summary>
        [DataMember]
        public OrderDto[] Orders;


        [DataMember]
        public OrderDto Order;
    }
}
