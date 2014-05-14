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
    public partial class Orders : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the selected menu item in Master page.
                Master.TheMenuInMasterPage.SelectedItem = "orders";

                // Set the default sort settings
                SortColumn = "CustomerId";
                SortDirection = "ASC";

                Bind();
            }
        }

        /// <summary>
        /// Sets datasources and bind data to controls.
        /// </summary>
        private void Bind()
        {
            // Retrieve orders via Customer Facade. 
            CustomerController controller = new CustomerController();
            GridViewOrders.DataSource = controller.GetCustomersWithOrderStatistics(SortExpression);
            GridViewOrders.DataBind();
        }

        #region Sorting

        /// <summary>
        /// Sets sort order and re-binds page.
        /// </summary>
        protected void GridViewOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
            SortColumn = e.SortExpression;

            Bind();
        }

        /// <summary>
        /// Adds glyph to header according to current sort settings.
        /// </summary>
        protected void GridViewOrders_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                AddGlyph(this.GridViewOrders, e.Row);
            }
        }

        #endregion
    }
}
