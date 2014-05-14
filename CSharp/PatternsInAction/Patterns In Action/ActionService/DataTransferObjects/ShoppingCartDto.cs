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
    /// Shopping Cart Data Transfer Object.
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
    [DataContract(Name = "ShoppingCart", Namespace = "http://www.yourcompany.com/types/")]
    public class ShoppingCartDto
    {
        [DataMember]
        public double Shipping { get; set; }

        [DataMember]
        public double SubTotal { get; set; }

        [DataMember]
        public double Total { get; set; }

        [DataMember]
        public string ShippingMethod { get; set; }

        [DataMember]
        public ShoppingCartItemDto[] CartItems { get; set; }
    }
}

