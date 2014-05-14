using System;
using System.Collections.Generic;
using System.Text;

namespace Cart
{
    /// <summary>
    /// Defines methods to determine shipping costs.
    /// </summary>
    public interface IShipping 
    {
        /// <summary>
        /// Estimates shipping costs given a product unit price and quantity.
        /// </summary>
        /// <param name="unitPrice">Unit price of product.</param>
        /// <param name="quantity">Quantity ordered.</param>
        /// <returns>Estimated shipping cost.</returns>
        double EstimateShipping(double unitPrice, int quantity);

        /// <summary>
        /// Calculates shipping costs given zip codes and product dimensions. 
        /// </summary>
        /// <param name="fromZip">Zip code of warehouse.</param>
        /// <param name="toZip">Zip code of customer.</param>
        /// <param name="weight">Product weight.</param>
        /// <param name="size">Product size.</param>
        /// <returns>Shipping costs.</returns>
        double CalculateShipping(string fromZip, string toZip, double weight, double size);
    }
}
