using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace AspControls
{
    /// <summary>
    /// Menu control. Renders HTML menu controls including selected menu items,
    /// menu item indentation, as well as the proper HTML and CSS (cascading 
    /// style sheet) tags.
    /// </summary>
    /// <remarks>
    /// GoF Design patterns: Composite.
    /// EnterPrise Design Pattern: Transform View.
    /// The Composite Design Pattern is a self-referencing data structure which 
    /// in this case is a menu tree hierarchy of self-referencing menu items. 
    /// 
    /// The Transform View processes data elements and transforms these into HTML.  
    /// Usually this applies to domain specific data (business objects), but it is 
    /// valid also for menu items. In fact, all databound Web Controls in ASP.NET 
    /// are pure TransForm View Design Pattern implementations.
    /// </remarks>
    [DefaultProperty("Selected")]
    [ToolboxData("<{0}:MenuComposite runat=server></{0}:MenuComposite>")]
    public class MenuComposite : WebControl
    {
        /// <summary>
        /// Gets and sets the selected menu item.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string SelectedItem
        {
            get
            {
                String s = (String)ViewState["SelectedItem"];
                return ((s == null) ? String.Empty : s);
            }

            set
            {
                ViewState["SelectedItem"] = value;
            }
        }

        /// <summary>
        /// Gets and sets the entire menu tree using the ASP.NET Viewstate.
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(null)]
        [Localizable(true)]
        public MenuCompositeItem MenuItems
        {
            get
            {
                return ViewState["MenuItems"] as MenuCompositeItem;
            }

            set
            {
                ViewState["MenuItems"] = value;
            }
        }

        /// <summary>
        /// Renders the entire menu structure.
        /// </summary>
        /// <param name="output">The HTML output stream</param>
        protected override void RenderContents(HtmlTextWriter output)
        {
            MenuCompositeItem root = this.MenuItems;

            output.Write(@"<div id=""navcontainer"">");
            output.Write(@"	<ul id=""navlist"">");

            RecursiveRender(output, root, 0);

            output.Write(@"	</ul>");
            output.Write(@"</div>");
        }

        /// <summary>
        /// Recursive function that renders a menu item at the correct 
        /// indentation level.  This is a private helper function.
        /// </summary>
        /// <param name="output">The HTML output stream.</param>
        /// <param name="item">Menu item.</param>
        /// <param name="depth">Indentation depth.</param>
        private void RecursiveRender(HtmlTextWriter output, MenuCompositeItem item, int depth)
        {
            if (depth > 0) // Skip root node 
            {
                if (depth == 1)
                    output.Write("<li>");  // main menu
                else
                    output.Write(@"<li class=""indented"">");  // sub menu

                output.Write(@"<a href=""" + item.Link + @""">");

                if (item.Item == SelectedItem)  // selected item
                    output.Write(@"<span class=""selected"">" + item.Item + "</span>");
                else
                    output.Write(item.Item);  // unselected item.

                output.Write("</a>");
            }

            // Recursively iterate over its children.
            for (int i = 0; i < item.Children.Count; i++)
            {
                RecursiveRender(output, item.Children[i], depth + 1);
            }
        }
    }
}
