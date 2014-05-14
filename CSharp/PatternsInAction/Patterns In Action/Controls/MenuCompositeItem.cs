using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AspControls
{
    /// <summary>
    /// Represents a menu item. This is a node in a tree of menu items. 
    /// Menu items can have children themselves.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Composite.
    /// </remarks>
    [Serializable()]
    public class MenuCompositeItem
    {
        /// <summary>
        /// Constructor of menu item.
        /// </summary>
        /// <param name="item">Menu item display name.</param>
        /// <param name="link">Menu item link</param>
        public MenuCompositeItem(string item, string link)
        {
            Children = new List<MenuCompositeItem>();

            Item = item;
            Link = link;
        }

        /// <summary>
        /// Gets and set menu item display name.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Gets and sets menu item link.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets and set list of child menu items.
        /// Composite Design Pattern. It is trought the Children property 
        /// that an item can reference an entire list of the same objects. 
        /// </summary>
        public List<MenuCompositeItem> Children{ get; set; }
    }
}
