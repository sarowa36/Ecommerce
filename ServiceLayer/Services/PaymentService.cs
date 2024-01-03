using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Base.Services;

namespace ServiceLayer.Services
{
    public class PaymentService : IPaymentService
    {
        readonly IShoppingCartItemReadRepository _shoppingCartItemReadRepository;
        readonly Options options = new Options();

        public PaymentService(IShoppingCartItemReadRepository shoppingCartItemReadRepository, IIdentityService identityService)
        {
            options = new Options()
            {
                ApiKey = "sandbox-Zm7KunbUsO9guZthOmg1Ri5aFAqTCgUl",
                SecretKey = "sandbox-l0zHbCeSwSduPOhpXQDFw6wJ0NMZhUQy",
                BaseUrl = "https://sandbox-api.iyzipay.com",
            };
            _shoppingCartItemReadRepository = shoppingCartItemReadRepository;
        }

        public async Task<CheckoutFormInitialize> StartPayment(ApplicationUser user,List<ShoppingCartItem> cartItems)
        {
            var totalprice = cartItems.Sum(x => x.Product.Price);
            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest()
            {
                Locale = Locale.TR.ToString(),
                Price = "2",
                PaidPrice = "2.2",
                Currency = Currency.TRY.ToString(),
                BasketId = "B67832",
                PaymentGroup = PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = "https://localhost:7121/api/User/Payment/callback",
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
            cartItems.ForEach(item => request.BasketItems.Add(new BasketItem()
            {
                Id = item.ProductId.ToString(),
                Name = item.Product.Name,
                ItemType = BasketItemType.PHYSICAL.ToString(),
                Category1="Clothes",
                Price = "1",
            }));
            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);

            return checkoutFormInitialize;
        }
        public async Task<CheckoutForm> RetrivePayment(RetrieveCheckoutFormRequest req)
        {
            var retrive = CheckoutForm.Retrieve(req, options);
            return retrive;
        }
    }
}
