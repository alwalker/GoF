using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ComponentModel;

using ASPNETWebApplication.ActionServiceReference;

namespace ASPNETWebApplication.Controllers
{
    /// <summary>
    /// Controller for the shopping cart.
    /// </summary>
    /// <remarks>
    /// MV Patterns: Model View Controller Pattern.
    /// This is a 'loose' implementation of the MVC pattern.
    /// </remarks>
    [DataObject(true)]
    public class CartController : ControllerBase
    {
        /// <summary>
        /// Gets the user's cart.
        /// </summary>
        /// <returns>Shopping cart.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public ShoppingCart GetCart()
        {
            CartRequest request = new CartRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Read";

            CartResponse response = ActionServiceClient.GetCart(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetCart: Request and CorrelationId do not match.");

            return response.Cart;
        }

        /// <summary>
        /// Adds an item to the shopping cart.
        /// </summary>
        /// <param name="productId">Unique product identifier or item.</param>
        /// <param name="name">Item name.</param>
        /// <param name="quantity">Quantity of items.</param>
        /// <param name="unitPrice">Unit price for each item.</param>
        /// <returns>Updated shopping cart.</returns>
        public ShoppingCart AddItem(int productId, string name, int quantity, double unitPrice)
        {
            CartRequest request = new CartRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Create";
            request.CartItem = new ShoppingCartItem { Id = productId, Name = name, Quantity = quantity, UnitPrice = unitPrice };

            CartResponse response = ActionServiceClient.SetCart(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("AddItem: Request and CorrelationId do not match.");

            return response.Cart;
        }

        /// <summary>
        /// Removes a line item from the shopping cart.
        /// </summary>
        /// <param name="productId">The item to be removed.</param>
        /// <returns>Updated shopping cart.</returns>
        public ShoppingCart RemoveItem(int productId)
        {
            CartRequest request = new CartRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Delete";
            request.CartItem = new ShoppingCartItem { Id = productId };

            CartResponse response = ActionServiceClient.SetCart(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("RemoveItem: Request and CorrelationId do not match.");

            return response.Cart;
        }

        /// <summary>
        /// Updates a line item in the shopping cart with a new quantity.
        /// </summary>
        /// <param name="productId">Unique product line item.</param>
        /// <param name="quantity">New quantity.</param>
        /// <returns>Updated shopping cart.</returns>
        public ShoppingCart UpdateQuantity(int productId, int quantity)
        {
            CartRequest request = new CartRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Update";
            request.CartItem = new ShoppingCartItem { Id = productId, Quantity = quantity };

            CartResponse response = ActionServiceClient.SetCart(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("UpdateItem: Request and CorrelationId do not match.");

            return response.Cart;
        }

        /// <summary>
        /// Sets shipping method used to compute shipping charges.
        /// </summary>
        /// <param name="shippingMethod">The name of the shipper.</param>
        /// <returns>Updated shopping cart.</returns>
        public ShoppingCart SetShippingMethod(string shippingMethod)
        {
            CartRequest request = new CartRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.Action = "Update";
            request.ShippingMethod = shippingMethod;

            CartResponse response = ActionServiceClient.SetCart(request);

            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("SetShippingMethod: Request and CorrelationId do not match.");

            return response.Cart;
        }
    }
}
