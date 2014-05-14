using System;
using System.Data;
using System.Configuration;
using System.Reflection;

using System.Runtime.Serialization;

namespace ActionService.MessageBase
{
    /// <summary>
    /// Base class for all response messages to clients of the web service. It standardizes 
    /// communication between web services and clients with a series of common values and 
    /// their initial defaults. Derived response message classes can override the default 
    /// values if necessary.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class ResponseBase
    {
        /// <summary>
        /// A flag indicating success or failure of the web service response back to the 
        /// client. Default is success. Ebay.com uses this model.
        /// </summary>
        [DataMember]
        public AcknowledgeType Acknowledge = AcknowledgeType.Success;

        /// <summary>
        /// CorrelationId mostly returns the RequestId back to client. 
        /// </summary>
        [DataMember]
        public string CorrelationId;

        /// <summary>
        /// Message back to client. Mostly used when a web service failure occurs. 
        /// </summary>
        [DataMember]
        public string Message;

        /// <summary>
        /// Reservation number issued by the web service. Used in long running requests. 
        /// Also sometimes referred to as Correlation Id. This number is a way for both the client
        /// and web service to keep track of long running requests (for example, a request 
        /// to make a reservation for a airplane flight).
        /// </summary>
        [DataMember]
        public string ReservationId;

        /// <summary>
        /// Date when reservation number expires. 
        /// </summary>
        [DataMember]
        public DateTime ReservationExpires;

        /// <summary>
        /// Version number (in major.minor format) of currently executing web service. 
        /// Used to offer a level of understanding (related to compatibility issues) between
        /// the client and the web service as the web services evolve over time. 
        /// Ebay.com uses this in their API.
        /// </summary>
        [DataMember]
        public string Version =
            Assembly.GetExecutingAssembly().GetName().Version.Major + "." +
            Assembly.GetExecutingAssembly().GetName().Version.Minor;

        /// <summary>
        /// Build number of currently executing web service. Used as an indicator
        /// to client whether certain code fixes are included or not.
        /// Ebay.com uses this in their API.
        /// </summary>
        [DataMember]
        public string Build =
            Assembly.GetExecutingAssembly().GetName().Version.Build.ToString();


        /// <summary>
        /// Number of rows affected by "Create", "Update", or "Delete" action.
        /// </summary>
        [DataMember]
        public int RowsAffected;
    }
}
