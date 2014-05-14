using System;
using System.Data;
using System.Configuration;

using System.Runtime.Serialization;

using ActionService.MessageBase;

namespace ActionService.Messages
{
    /// <summary>
    /// Represents a logout response message from web service to client.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class LogoutResponse : ResponseBase
    {
        // This derived class intentionally left blank. 
        // Base class has the required parameters to acknowledge.
    }
}