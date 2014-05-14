using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

using BusinessObjects;

using ActionService.MessageBase;
using ActionService.DataTransferObjects;

namespace ActionService.Messages
{
    /// <summary>
    /// Represents a product response message from web service to client.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class ProductResponse : ResponseBase
    {
        [DataMember]
        public IList<CategoryDto> Categories; // { get; set; }

        [DataMember]
        public IList<ProductDto> Products; // { get; set; }

        [DataMember]
        public ProductDto Product; // { get; set; }
    }
}
