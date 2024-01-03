using EntityLayer.Entities;
using Iyzipay.Model;
using Iyzipay.Request;

namespace ServiceLayer.Base.Services
{
    public interface IPaymentService
    {
        Task<CheckoutFormInitialize> StartPayment(ApplicationUser user);
        Task<CheckoutForm> RetrivePayment(RetrieveCheckoutFormRequest req);
    }
}