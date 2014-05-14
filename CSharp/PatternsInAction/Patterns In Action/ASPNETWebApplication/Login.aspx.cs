using System;
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
    public partial class Login : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the selected menu item in Master page.
                Master.TheMenuInMasterPage.SelectedItem = "login";

                Tries = 0;

                // Put cursor in first field
                TextboxUserName.Focus();
            }
        }

        protected void ButtonSubmit_Click(object sender, System.EventArgs e)
        {
            string username = TextboxUserName.Text.Trim();
            string password = TextboxPassword.Text.Trim();

            AuthenticationController controller = new AuthenticationController();

            if (controller.Login(username, password)) 
            {
                if (FormsAuthentication.GetRedirectUrl(username, false).IndexOf("WebAdmin") > 0)
                    FormsAuthentication.RedirectFromLoginPage(username, false);
                else
                    Response.Redirect(ResolveUrl("~/WebAdmin/Admin.aspx"));
            }
            else
            {
                if (Tries >= 2)
                    Response.Redirect(ResolveUrl("~/Default.aspx"));
                else
                {
                    Tries += 1;
                    this.LiteralError.Text = "Invalid Username or Password. Please try again.";
                }
            }
        }

        // Counter for number of login attempts.
        private int Tries
        {
            get { return int.Parse(ViewState["Tries"].ToString()); }
            set { ViewState["Tries"] = value; }
        }
    }
}
