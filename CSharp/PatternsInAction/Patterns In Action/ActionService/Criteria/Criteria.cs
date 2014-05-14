using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ActionService.Criteria
{
    /// <summary>
    /// Base class that holds criteria for queries.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class Criteria
    {
        [DataMember]
        public string SortExpression { get; set; }
    }
}