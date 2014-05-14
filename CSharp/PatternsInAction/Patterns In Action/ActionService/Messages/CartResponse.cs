using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

using Cart;

using System.Runtime.Serialization;

using ActionService.MessageBase;
using ActionService.DataTransferObjects;

namespace ActionService.Messages
{
    /// <summary>
    /// Represents a shopping cart message response to client.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class CartResponse : ResponseBase
    {
        [DataMember]
        public ShoppingCartDto Cart;
    }
}