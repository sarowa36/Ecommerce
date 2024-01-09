using EntityLayer.Entities;
using Iyzipay.Model;
using Iyzipay.Request;

namespace ServiceLayer.Base.Services
{
    public interface IPaymentService
    {
        Task<CheckoutFormInitialize> StartPayment(Order order);
        Task<CheckoutForm> ProcessPayment(RetrieveCheckoutFormRequest req);
    }
}