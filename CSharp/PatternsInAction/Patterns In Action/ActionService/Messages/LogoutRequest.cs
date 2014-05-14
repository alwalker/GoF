using System;
using System.Data;
using System.Configuration;

using System.Runtime.Serialization;
using ActionService.MessageBase;

namespace ActionService.Messages
{
    /// <summary>
    /// Respresents a logout request message from client to web service.
    /// </summary>
    [DataContract(Namespace = "http://www.yourcompany.com/types/")]
    public class LogoutRequest : RequestBase
    {
        // This derived class intentionally left blank
        // Base class has the required parameters.
    }
}
