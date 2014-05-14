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

using ASPNETWebApplication.ActionServiceReference;
using ASPNETWebApplication.Controllers;

namespace ASPNETWebApplication.WebAdmin
{
    public partial class Order : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the selected menu item in the Master page.
                Master.TheMenuInMasterPage.SelectedItem = "orders";

                // Save off customerId 
                CustomerId = int.Parse(Request["id"].ToString());

                Bind();
            }
        }

        /// <summary>
        /// Sets datasources and bind data to controls.
        /// </summary>
        private void Bind()
        {
            // Get customer via Customer Controller.
            CustomerController controller = new CustomerController();
            ActionServiceReference.Customer customer = controller.GetCustomerWithOrders(CustomerId);

            // Set company name
            LabelHeader.Text = "<font color='black'>Orders for:</font> "
                + customer.Company + " (" + customer.Country + ")";

            GridViewOrders.DataSource = customer.Orders;
            GridViewOrders.DataBind();
        }

        /// <summary>
        /// Gets or sets customerId for the page in Session.
        /// </summary>
        private int CustomerId
        {
            get { return int.Parse(Session["customerId"].ToString()); }
            set { Session["customerId"] = value; }
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink link = e.Row.Cells[4].Controls[0] as HyperLink;
                link.NavigateUrl += "&cid=" + CustomerId;
            }
        }
    }
}
