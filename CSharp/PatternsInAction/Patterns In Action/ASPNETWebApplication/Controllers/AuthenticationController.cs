using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using ASPNETWebApplication.ActionServiceReference;

namespace ASPNETWebApplication.Controllers
{
    /// <summary>
    /// Controller class for user authentication.
    /// </summary>
    /// <remarks>
    /// MV Pattern: Model View Controller Pattern.
    /// This is a 'loose' implementation of the MVC pattern.
    /// </remarks>
    public class AuthenticationController : ControllerBase
    {
        /// <summary>
        /// GetToken must be the first call into web service. 
        /// This is irrespective of whether user is logging in or not.
        /// </summary>
        /// <returns>Unique access token that is valid for the duration of the session.</returns>
        public string GetToken()
        {
            TokenRequest request = new TokenRequest();
            request.RequestId = NewRequestId;
            request.ClientTag = ClientTag;

            TokenResponse response = ActionServiceClient.GetToken(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetToken: RequestId and CorrelationId do not match.");

            return response.AccessToken;
        }

        /// <summary>
        /// Login to the system.
        /// </summary>
        /// <param name="username">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Success or failure flag.</returns>
        public bool Login(string username, string password)
        {
            LoginRequest request = new LoginRequest();
            request.RequestId = NewRequestId;
            request.ClientTag = ClientTag;
            request.AccessToken = AccessToken;

            request.UserName = username;
            request.Password = password;

            LoginResponse response = ActionServiceClient.Login(request);

            return (response.Acknowledge == AcknowledgeType.Success);
        }

        /// <summary>
        /// Logout from from the system.
        /// </summary>
        /// <returns>Success or failure flag.</returns>
        public bool Logout()
        {
            LogoutRequest request = new LogoutRequest();
            request.RequestId = NewRequestId;
            request.ClientTag = ClientTag;
            request.AccessToken = AccessToken;

            LogoutResponse response = ActionServiceClient.Logout(request);

            return (response.Acknowledge == AcknowledgeType.Success);
        }
    }
}