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
    /// Base class for all Controllers. 
    /// Manages ClientTag, AccessToken, and RequestIds.
    /// </summary>
    public class ControllerBase
    {
        /// <summary>
        /// Client tag provided by the service provider and stored locally. 
        /// This value must be provided with every service call.
        /// </summary>
        protected static string ClientTag { get; private set; }

        /// <summary>
        /// Static constructor
        /// </summary>
        static ControllerBase()
        {
            // Retrieve ClientTag from web config file
            ClientTag = ConfigurationManager.AppSettings.Get("ClientTag");
        }

        // The access token that was returned from the service.
        // This value must be provided in every call for the duration of the session.
        //private string _accessToken;

        /// <summary>
        /// Gets or sets access token. If no token exists a new one is retrieved from service.
        /// </summary>
        protected string AccessToken
        {
            get
            {
                if (HttpContext.Current.Session["AccessToken"] == null)
                {
                    // Request a unique accesstoken from the webservice. This token is
                    // that is valid for the duration of the session.
                    AuthenticationController controller = new AuthenticationController();
                    HttpContext.Current.Session["AccessToken"] = controller.GetToken(); 
                }
                return (string)HttpContext.Current.Session["AccessToken"];
            }
        }

        /// <summary>
        /// Gets a new random GUID request id.
        /// </summary>
        protected string NewRequestId
        {
            get { return Guid.NewGuid().ToString(); }
        }

        /// <summary>
        /// Lazy loads ActionServiceClient and stores it in Session object.
        /// </summary>
        protected ActionServiceClient ActionServiceClient
        {
            get
            {
                if (HttpContext.Current.Session["ActionServiceClient"] == null)
                    HttpContext.Current.Session["ActionServiceClient"] = new ActionServiceClient();

                return HttpContext.Current.Session["ActionServiceClient"] as ActionServiceClient;
            }
        }
    }
}
