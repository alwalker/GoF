using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization;

using ActionService.MessageBase;
using ActionService.DataTransferObjects;

namespace ActionService.Messages
{

    /// <summary>
    /// Represents a shopping cart request message from client.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class CartRequest : RequestBase
    {
        [DataMember]
        public ShoppingCartItemDto CartItem { get; set; }

        [DataMember]
        public string ShippingMethod { get; set; }
    }
}