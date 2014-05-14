﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

using ASPNETWebApplication.Controllers;

namespace ASPNETWebApplication
{
    public partial class Logout : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthenticationController controller = new AuthenticationController();
            controller.Logout();

            FormsAuthentication.SignOut();
        }
    }
}
