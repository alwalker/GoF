using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ActionService.Criteria
{
    /// <summary>
    /// Holds criteria for customer queries.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class CustomerCriteria : Criteria
    {
        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public bool IncludeOrderStatistics { get; set; }
    }
}
