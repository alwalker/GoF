using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cart
{
    /// <summary>
    /// United Postal Service specific class to determine shipping costs.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Strategy.
    /// </remarks>
    public class ShippingStrategyUPS : IShipping
    {
        /// <summary>
        /// Estimates shipping costs given a product unit price and quantity.
        /// </summary>
        /// <param name="unitPrice">Unit price of product.</param>
        /// <param name="quantity">Quantity ordered</param>
        /// <returns>Estimated shipping cost.</returns>
        public double EstimateShipping(double unitPrice, int quantity)
        {
            return (unitPrice * .09) * (double)quantity;
        }

        /// <summary>
        /// Calculates shipping costs given zip codes and product dimensions. 
        /// </summary>
        /// <param name="fromZip">Zip code of warehouse.</param>
        /// <param name="toZip">Zip code of customer.</param>
        /// <param name="weight">Product weight.</param>
        /// <param name="size">Product size.</param>
        /// <returns>Shipping costs.</returns>
        public double CalculateShipping(string fromZip, string toZip, double weight, double size)
        {
            throw new NotImplementedException("ShippingStrategyUPS.CalculateShipping is not implemented.");
        }
    }
}
