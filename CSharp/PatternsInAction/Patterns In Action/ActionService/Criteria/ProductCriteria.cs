using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ActionService.Criteria
{
    /// <summary>
    /// Holds criteria for product queries.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class ProductCriteria : Criteria
    {
        [DataMember]
        public int CategoryId { get; set; }
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductName { get; set; }
        [DataMember]
        public double PriceFrom { get; set; }
        [DataMember]
        public double PriceThru { get; set; }
    }
}