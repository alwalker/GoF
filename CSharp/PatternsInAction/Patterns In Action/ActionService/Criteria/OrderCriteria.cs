using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ActionService.Criteria
{
    /// <summary>
    /// Holds criteria for order queries.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class OrderCriteria : Criteria
    {
        [DataMember]
        public int OrderId { get; set; }


        [DataMember]
        public int CustomerId { get; set; }
    }
}