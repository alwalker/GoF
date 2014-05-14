using System;
using System.Data;
using System.Configuration;

using System.Runtime.Serialization;
using ActionService.MessageBase;

namespace ActionService.Messages
{

    /// <summary>
    /// Represents a login response message to the client.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class LoginResponse : ResponseBase
    {
        /// <summary>
        /// Uri to which client should redirect following successful login. 
        /// This would be necessary if authentication is handled centrally 
        /// and other services are distributed accross multiple servers. 
        /// Not used in this sample application. 
        /// SalesForce.com uses this in their API.
        /// </summary>
        [DataMember]
        public string Uri = "";

        /// <summary>
        /// Session identifier. Useful when sessions are maintained using 
        /// SOAP headers (rather than cookies). Not used in this sample application.
        /// SalesForce.com uses this in their SOAP header model.
        /// </summary>
        [DataMember]
        public string SessionId = "";
    }
}
