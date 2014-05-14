using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cart
{
    /// <summary>
    /// Respresents a line item in a shopping cart
    /// </summary>
    public class ShoppingCartItem
    {
        /// <summary>
        /// Unique identifier of the product;
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Quantity of products.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Price per unit for product.
        /// </summary>
        public double UnitPrice { get; set; }
    }
}
