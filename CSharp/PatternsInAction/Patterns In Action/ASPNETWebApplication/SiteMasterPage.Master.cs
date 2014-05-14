using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using AspControls;

namespace ASPNETWebApplication
{
    public partial class SiteMasterPage : System.Web.UI.MasterPage
    {
        /// <summary>
        /// Establishes the composite menu hierarchy which is present on all pages.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Build the composite menu tree
                // This tree implements the Composite Design Pattern.
                MenuCompositeItem root = new MenuCompositeItem("root", null);
                MenuCompositeItem home = new MenuCompositeItem("home", ResolveUrl("~/Default.aspx"));
                MenuCompositeItem shop = new MenuCompositeItem("shopping", ResolveUrl("~/WebShop/Shopping.aspx"));
                MenuCompositeItem prod = new MenuCompositeItem("products", ResolveUrl("~/WebShop/Products.aspx"));
                MenuCompositeItem srch = new MenuCompositeItem("search", ResolveUrl("~/WebShop/Search.aspx"));
                MenuCompositeItem cart = new MenuCompositeItem("cart", ResolveUrl("~/WebShop/Cart.aspx"));
                MenuCompositeItem admn = new MenuCompositeItem("administration", ResolveUrl("~/WebAdmin/Admin.aspx"));
                MenuCompositeItem cust = new MenuCompositeItem("customers", ResolveUrl("~/WebAdmin/Customers.aspx"));
                MenuCompositeItem ordr = new MenuCompositeItem("orders", ResolveUrl("~/WebAdmin/Orders.aspx"));

                MenuCompositeItem auth;
                if (Request.IsAuthenticated)
                    auth = new MenuCompositeItem("logout", ResolveUrl("~/Logout.aspx"));
                else
                    auth = new MenuCompositeItem("login", ResolveUrl("~/Login.aspx"));

                shop.Children.Add(prod);
                shop.Children.Add(srch);
                shop.Children.Add(cart);
                admn.Children.Add(cust);
                admn.Children.Add(ordr);
                root.Children.Add(home);
                root.Children.Add(shop);
                root.Children.Add(admn);
                root.Children.Add(auth);

                TheMenuComposite.MenuItems = root;
            }
        }

        /// <summary>
        /// Gets the menu from the master page. This property makes the menu 
        /// accessible from contentplaceholders. This allows the individual pages 
        /// to set the selected menu item.
        /// </summary>
        public MenuComposite TheMenuInMasterPage
        {
            get { return this.TheMenuComposite; }
        }

        /// <summary>
        /// Gets the page render time.
        /// </summary>
        protected string PageRenderTime
        {
            get
            {
                // Be sure that all ContentPlaceHolder pages are derived from PageBase.
                // BTW: this is how you access content pages from a master page --
                // most developers ask about access the other way around, that is, access 
                // the master page from the content pages which is also demonstrated here 
                // with the above TheMenuInMasterPage property.
                try
                {
                    PageBase pageBase = this.ContentPlaceHolder1.Page as PageBase; //(this.FindControl("ContentPlaceHolder1") as ContentPlaceHolder).Page as PageBase;// 
                    return pageBase.PageRenderTime;
                }
                catch { /* do nothing */ }

                return "";
            }
        }
    }
}
