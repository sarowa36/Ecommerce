using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using System.Globalization;

namespace ServiceLayer.Services
{
    public class PaymentService : IPaymentService
    {
        readonly IShoppingCartItemReadRepository _shoppingCartItemReadRepository;
        readonly Options options = new Options();
        readonly IServiceErrorContainer _serviceErrorContainer;
        readonly IHttpContextAccessor _httpContextAccessor;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        public PaymentService(IShoppingCartItemReadRepository shoppingCartItemReadRepository,
            IServiceErrorContainer serviceErrorContainer,
            IHttpContextAccessor httpContext)
        {
            options = new Options()
            {
                ApiKey = "sandbox-Zm7KunbUsO9guZthOmg1Ri5aFAqTCgUl",
                SecretKey = "sandbox-l0zHbCeSwSduPOhpXQDFw6wJ0NMZhUQy",
                BaseUrl = "https://sandbox-api.iyzipay.com",
            };
            _shoppingCartItemReadRepository = shoppingCartItemReadRepository;
            _serviceErrorContainer = serviceErrorContainer;
            _httpContextAccessor = httpContext;
        }

        public async Task<CheckoutFormInitialize> StartPayment(Order order)
        {
            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest()
            {
                Locale = Locale.TR.ToString(),
                Price = order.TotalPrice.ToString(CultureInfo.GetCultureInfo("en-US")),
                PaidPrice = order.PaidPrice.ToString(CultureInfo.GetCultureInfo("en-US")),
                Currency = Currency.TRY.ToString(),
                BasketId = order.Id.ToString(),
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = "https://"+HttpContext.Request.Host+"/api/User/Payment/callback",
                EnabledInstallments = new List<int>() { 1, 2, 3, 4, 5, 6 },
                Buyer = new Buyer()
                {
                    Id = "BY789",
                    Name = "John",
                    Surname = "Doe",
                    GsmNumber = "+905350000000",
                    Email = "email@email.com",
                    IdentityNumber = "74300864791",
                    LastLoginDate = "2015-10-05 12:43:35",
                    RegistrationDate = "2013-04-21 15:12:09",
                    RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                    Ip = "85.34.78.112",
                    City = "Istanbul",
                    Country = "Turkey",
                    ZipCode = "34732",
                },
                ShippingAddress = new Address()
                {
                    ContactName = "Jane Doe",
                    City = "Istanbul",
                    Country = "Turkey",
                    Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                    ZipCode = "34742",
                },
                BillingAddress = new Address()
                {
                    ContactName = "Jane Doe",
                    City = "Istanbul",
                    Country = "Turkey",
                    Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1",
                    ZipCode = "34742",
                },
                BasketItems = new List<BasketItem>()
            };
            order.OrderItems.ForEach(item =>
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    request.BasketItems.Add(new BasketItem()
                    {
                        Id = item.ProductId.ToString(),
                        Name = item.Product.Name,
                        ItemType = BasketItemType.PHYSICAL.ToString(),
                        Category1 = "Clothes",
                        Price = item.Price.ToString(CultureInfo.GetCultureInfo("en-US")),
                    });
                }
            });
            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            return checkoutFormInitialize;

        }
        public async Task<CheckoutForm> ProcessPayment(RetrieveCheckoutFormRequest req)
        {
            var retrive = CheckoutForm.Retrieve(req, options);
            return retrive;
        }
    }
}
