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
    public partial class Product : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set the selected menu item in Master page.
                Master.TheMenuInMasterPage.SelectedItem = "products";

                // Save off ProductId for this page.
                ProductId = int.Parse(Request["id"].ToString());

                // This page is also accessible from Cart page
                if (Request["HTTP_REFERER"].ToString().Contains("Cart.aspx"))
                    HyperLinkBack.Text = "&lt; back to shopping cart";

                // Get product image from image service. This demo supplies just a single image.
                ImageProduct.ImageUrl = imageService + "GetProductImage/" + ProductId;
            }
        }

        /// <summary>
        /// Adds item to shopping cart and redirect to shopping cart page.
        /// </summary>
        protected void ButtonAddToCart_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid) return;

            // Retrieve product via Product Facade.
            ProductController controller = new ProductController();
            ActionServiceReference.Product product = controller.GetProduct(ProductId);

            // Get product details and add information to cart.
            int productId = product.ProductId;
            string name = product.ProductName;
            double unitPrice = product.UnitPrice;

            int quantity;
            if (!int.TryParse(TextBoxQuantity.Text.Trim(), out quantity))
                quantity = 1;

            CartController cartController = new CartController();
            cartController.AddItem(productId, name, quantity, unitPrice);

            // Show shopping cart to user.
            Response.Redirect("Cart.aspx");
        }

        /// <summary>
        /// Gets and sets productId to Session.
        /// </summary>
        private int ProductId
        {
            set { Session["productId"] = value; }
            get { return int.Parse(Session["productId"].ToString()); }
        }
    }
}
