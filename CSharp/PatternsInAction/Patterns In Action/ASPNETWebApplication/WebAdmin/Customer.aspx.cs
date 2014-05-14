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
using ASPNETWebApplication.ActionServiceReference;

namespace ASPNETWebApplication.WebAdmin
{
    public partial class Customer : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the selected menu item in the Master page.
                Master.TheMenuInMasterPage.SelectedItem = "customers";

                CustomerId = int.Parse(Request["id"].ToString());

                // Set DetailsView control in Add or Edit mode.
                if (CustomerId == 0)
                    DetailsViewCustomer.ChangeMode(DetailsViewMode.Insert);
                else
                    DetailsViewCustomer.ChangeMode(DetailsViewMode.Edit);

                // Set image
                ImageCustomer.ImageUrl = imageService + "GetCustomerImageLarge/" + CustomerId;
            }
        }

        /// <summary>
        /// Gets or sets the customerId for this page.
        /// </summary>
        private int CustomerId
        {
            get { return int.Parse(Session["CustomerId"].ToString()); }
            set { Session["CustomerId"] = value; }
        }

        /// <summary>
        /// Saves data for new or edited customer to database.
        /// </summary>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            CustomerController controller = new CustomerController();

            ActionServiceReference.Customer customer;
            if (CustomerId == 0)
                customer = new ActionServiceReference.Customer();
            else
                customer = controller.GetCustomer(CustomerId);

            // Get Company name from page.
            DetailsViewRow row = DetailsViewCustomer.Rows[1];
            TextBox textBox = row.Cells[1].Controls[0] as TextBox;
            customer.Company = textBox.Text.Trim();

            // Get City from page.
            row = DetailsViewCustomer.Rows[2];
            textBox = row.Cells[1].Controls[0] as TextBox;
            customer.City = textBox.Text.Trim();

            // Get Country from page.
            row = DetailsViewCustomer.Rows[3];
            textBox = row.Cells[1].Controls[0] as TextBox;
            customer.Country = textBox.Text.Trim();

            try
            {
                if (CustomerId == 0)
                    controller.AddCustomer(customer);
                else
                    controller.UpdateCustomer(customer);
            }
            catch (ApplicationException ex)
            {
                LabelError.Text = ex.Message.Replace(Environment.NewLine, "<br />");
                LabelError.Visible = true;
                return;
            }

            // Return to list of customers.
            Response.Redirect("Customers.aspx");

        }

        /// <summary>
        /// Cancel the page and redirect user to page with list of customers.
        /// </summary>
        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Customers.aspx");
        }

        /// <summary>
        /// Executed only once. Used to place cursor in first editable field.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DetailsView_OnDataBound(object sender, EventArgs e)
        {
            DetailsViewRow row = DetailsViewCustomer.Rows[1];
            TextBox textBox = row.Cells[1].Controls[0] as TextBox;
            textBox.Focus();
        }
    }
}
