using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cart
{
    /// <summary>
    /// Enumeration of different product shipping methods.
    /// </summary>
    public enum ShippingMethod
    {
        /// <summary>
        /// Shipping via Federal Express.
        /// </summary>
        Fedex,  

        /// <summary>
        /// Shipping via United Postal Services.
        /// </summary>
        UPS,

        /// <summary>
        /// Shipping via United States Postal Services.
        /// </summary>
        USPS    
    }
}
