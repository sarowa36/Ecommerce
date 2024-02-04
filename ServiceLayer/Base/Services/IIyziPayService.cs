using EntityLayer.Entities;
using Iyzipay.Model;
using Iyzipay.Request;

namespace ServiceLayer.Base.Services
{
    public interface IIyziPayService
    {
        Task<CheckoutFormInitialize> StartPayment(Order order, ApplicationUser user);
        Task<CheckoutForm> ProcessPayment(RetrieveCheckoutFormRequest req);
        Task<Refund> RefundOrder(OrderRefund orderRefund);
    }
}