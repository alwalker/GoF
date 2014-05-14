using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

using System.ServiceModel;
using ActionService.Messages;

namespace ActionService.ServiceContracts
{
    /// <summary>
    /// IService is the interface for Patterns in Action public services.
    /// </summary>
    /// <remarks>
    /// The Cloud Facade Pattern.
    /// </remarks>
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IActionService
    {
        [OperationContract]
        TokenResponse GetToken(TokenRequest request);

        [OperationContract]
        LoginResponse Login(LoginRequest request);

        [OperationContract]
        LogoutResponse Logout(LogoutRequest request);

        [OperationContract]
        CustomerResponse GetCustomers(CustomerRequest request);

        [OperationContract]
        CustomerResponse SetCustomers(CustomerRequest request);

        [OperationContract]
        OrderResponse GetOrders(OrderRequest request);

        [OperationContract]
        OrderResponse SetOrders(OrderRequest request);

        [OperationContract]
        ProductResponse GetProducts(ProductRequest request);

        [OperationContract]
        ProductResponse SetProducts(ProductRequest request);

        [OperationContract]
        CartResponse GetCart(CartRequest request);

        [OperationContract]
        CartResponse SetCart(CartRequest request);
    }
}