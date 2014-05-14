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
using System.Collections.Generic;
using System.ComponentModel;

using ASPNETWebApplication.ActionServiceReference;

namespace ASPNETWebApplication.Controllers
{
    /// <summary>
    /// Controller for customer orders.
    /// </summary>
    /// <remarks>
    /// MV Patterns: Model View Controller Pattern.
    /// This is a 'loose' implementation of the MVC pattern.
    /// </remarks>
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Gets a specific order.
        /// </summary>
        /// <param name="orderId">Unique order identifier.</param>
        /// <returns>The requested Order.</returns>
        [DataObjectMethod(DataObjectMethodType.Select)]
        public Order GetOrder(int orderId)
        {
            OrderRequest request = new OrderRequest();
            request.RequestId = NewRequestId;
            request.AccessToken = AccessToken;
            request.ClientTag = ClientTag;

            request.LoadOptions = new string[] { "Order", "Customer", "OrderDetails" };
            request.Criteria = new OrderCriteria { OrderId = orderId };

            OrderResponse response = ActionServiceClient.GetOrders(request);


            if (request.RequestId != response.CorrelationId)
                throw new ApplicationException("GetOrder: RequestId and CorrelationId do not match.");

            return response.Order;
        }
    }
}
