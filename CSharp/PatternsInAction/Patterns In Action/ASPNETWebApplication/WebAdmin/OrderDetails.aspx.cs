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
    public partial class OrderDetails : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the selected menu item in Master page.
                Master.TheMenuInMasterPage.SelectedItem = "orders";

                // Save off OrderId for this page.
                OrderId = int.Parse(Request["id"].ToString());
                CustomerId = int.Parse(Request["cid"].ToString());

                // Adjust breadcrumb now that parent CustomerId is known.
                SiteMapNode node = SiteMap.CurrentNode.ParentNode;
                node.ReadOnly = false; // Required to change node properties
                node.Url = "~/WebAdmin/Order.aspx?id=" + CustomerId;

                Bind();
            }
        }

        /// <summary>
        /// Sets datasources and bind data to controls.
        /// </summary>
        private void Bind()
        {
            OrderController controller = new OrderController();
            ActionServiceReference.Order order = controller.GetOrder(OrderId);

            // Set the date
            LabelHeader.Text = "Order Line Items"; 
            LabelOrderDate.Text = "Order date: " + order.OrderDate.ToShortDateString();
            HyperLinkBack.Text = "< back to orders "; 

            GridViewOrderDetails.DataSource = order.OrderDetails;
            GridViewOrderDetails.DataBind();
        }

        /// <summary>
        /// Gets or sets orderId
        /// </summary>
        private int OrderId { get; set; }

        /// <summary>
        /// Gets or sets customerId
        /// </summary>
        private int CustomerId { get; set; }
    }
}
