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

namespace ASPNETWebApplication.WebShop
{
    public partial class Cart : PageBase
    {
        private CartController _controller;
        private ShoppingCart _shoppingCart;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize controller and cart
            _controller = new CartController();
            _shoppingCart = _controller.GetCart();

            if (!IsPostBack)
            {
                // Set the selected menu item in Master page.
                Master.TheMenuInMasterPage.SelectedItem = "cart";

                Bind();
            }
        }

        /// <summary>
        /// Sets shopping cart datasource and databind to controls.
        /// </summary>
        private void Bind()
        {
            // Disable controls when cart is empty.
            if (_shoppingCart.CartItems.Length == 0)
            {
                HyperLinkCheckout.Enabled = false;
                HyperLinkCheckoutBottom.Enabled = false;
                LinkbuttonRecalculate.Enabled = false;
            }

            // Set the selected shipping method in dropdown control.
            if (_shoppingCart.ShippingMethod == "Fedex")
                DropDownListShipping.SelectedValue = "1";
            else if (_shoppingCart.ShippingMethod == "UPS")
                DropDownListShipping.SelectedValue = "2";
            else
                DropDownListShipping.SelectedValue = "3";

            // Databind cart to gridview control
            GridViewCart.DataSource = _shoppingCart.CartItems;
            Page.DataBind();
        }

        /// <summary>
        /// Gets the total cost of items in shopping cart.
        /// </summary>
        /// <returns>Total cost.</returns>
        protected double Total()
        {
            return _shoppingCart.Total;
        }

        /// <summary>
        /// Gets the subtotal cost of items in shopping cart.
        /// </summary>
        /// <returns>Subtotal cost.</returns>
        protected double SubTotal()
        {
            return _shoppingCart.SubTotal;
        }

        /// <summary>
        /// Gets the shipping cost (freight) for items in shopping cart.
        /// </summary>
        /// <returns>Shipping cost.</returns>
        protected double Shipping()
        {
            return _shoppingCart.Shipping;
        }

        /// <summary>
        /// Adjust shopping cart quantities and then recalculates totals and subtotals.
        /// </summary>
        protected void LinkbuttonRecalculate_Click(object sender, System.EventArgs e)
        {
            // Check if there are any items in the cart.
            if (_shoppingCart.CartItems.Length == 0) return;

            foreach (GridViewRow row in GridViewCart.Rows)
            {
                TextBox textBox = (TextBox)row.Cells[0].FindControl("TextBoxQuantity");

                int quantity;
                if (int.TryParse(textBox.Text, out quantity))
                {
                    int productId = int.Parse(((TextBox)row.Cells[0].FindControl("TextBoxId")).Text);

                    if (quantity <= 0)
                        _shoppingCart = _controller.RemoveItem(productId);
                    else if (quantity > 0 && quantity < 100)
                        _shoppingCart = _controller.UpdateQuantity(productId, quantity);
                }
            }
            Bind();
        }

        /// <summary>
        /// Changes to selected shipping method and rebinds new settings to page.
        /// </summary>
        protected void DropDownListShipping_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Change to selected shipping method (i.e. strategy pattern).
            string method = this.DropDownListShipping.SelectedItem.Text;

            _shoppingCart = _controller.SetShippingMethod(method);

            Bind();
        }

        /// <summary>
        /// Removes an item from the shopping cart.
        /// </summary>
        protected void GridViewCart_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow viewRow = GridViewCart.Rows[e.RowIndex];
            TextBox textBoxId = (TextBox)viewRow.Cells[0].FindControl("TextBoxId");

            int productId;
            if (int.TryParse(textBoxId.Text, out productId))
            {
                _shoppingCart = _controller.RemoveItem(productId);
                Bind();
            }
        }
    }
}
