using DataAccessLayer.Base.JsonData;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using System.Globalization;

namespace ServiceLayer.Services
{
    public class IyziPayService : IIyziPayService
    {
        readonly IShoppingCartItemReadRepository _shoppingCartItemReadRepository;
        readonly Options options = new Options();
        readonly IServiceErrorContainer _serviceErrorContainer;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly ICitiesAndDistrictsValues _citiesAndDistricts;
        readonly IConfiguration _configuration;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        public IyziPayService(IShoppingCartItemReadRepository shoppingCartItemReadRepository,
            IServiceErrorContainer serviceErrorContainer,
            IHttpContextAccessor httpContext,
            ICitiesAndDistrictsValues citiesAndDistricts,
            IConfiguration configuration)
        {
            _shoppingCartItemReadRepository = shoppingCartItemReadRepository;
            _serviceErrorContainer = serviceErrorContainer;
            _httpContextAccessor = httpContext;
            _citiesAndDistricts = citiesAndDistricts;
            _configuration = configuration;
            options = new Options()
            {
                ApiKey = _configuration["Iyzipay:ApiKey"],
                SecretKey = _configuration["Iyzipay:SecretKey"],
                BaseUrl = _configuration["Iyzipay:BaseUrl"],
            };
        }

        public async Task<CheckoutFormInitialize> StartPayment(Order order,ApplicationUser user)
        {
            var cityName = _citiesAndDistricts.GetCity(order.Address.CityId).Name;
            var districtName = _citiesAndDistricts.GetDistrict(order.Address.DistrictId).Name;
            var fullAddress = $"{cityName}/{districtName} Posta Kodu:{order.Address.Zip} {order.Address.Detail}";
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
                    Id = order.UserId,
                    Name = user.Name,
                    Surname = user.Surname,
                    GsmNumber = user.PhoneNumber,
                    Email = user.Email,
                    IdentityNumber = "11111111111",
                    RegistrationAddress = fullAddress,
                    Ip = GetIp(),
                    City =cityName,
                    Country = "Turkey",
                    ZipCode = order.Address.Zip.ToString(),
                },
                ShippingAddress = new Address()
                {
                    ContactName = order.TargetName,
                    City =cityName,
                    Country = "Turkey",
                    Description =fullAddress,
                    ZipCode = order.Address.Zip.ToString(),
                },
                BillingAddress = new Address()
                {
                    ContactName = order.TargetName,
                    City = cityName,
                    Country = "Turkey",
                    Description = fullAddress,
                    ZipCode = order.Address.Zip.ToString(),
                },
                BasketItems = new List<BasketItem>()
            };
            order.OrderItems.ForEach(item =>
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    request.BasketItems.Add(new BasketItem()
                    {
                        Id = item.Id.ToString(),
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
        public async Task<Refund> RefundOrder(OrderRefund orderRefund)
        {
            CreateAmountBasedRefundRequest refundRequest = new CreateAmountBasedRefundRequest() {
            Ip=GetIp(),
            PaymentId=orderRefund.Order.PaymentResult.PaymentId,
            Price=orderRefund.TotalRefundAmount.ToString(CultureInfo.GetCultureInfo("en-US")),
            };
            var refund=Refund.CreateAmountBasedRefundRequest(refundRequest, options);
            if(refund.Status== "success")
            {
                return refund;
            }
            else
            {
                _serviceErrorContainer.AddModelOnlyError(refund.ErrorMessage);
                return refund;
            }
        }
        public async Task<Refund> RefundOrder(Order order)
        {
            CreateAmountBasedRefundRequest refundRequest = new CreateAmountBasedRefundRequest()
            {
                Ip = GetIp(),
                PaymentId = order.PaymentResult.PaymentId,
                Price = order.TotalPrice.ToString(CultureInfo.GetCultureInfo("en-US")),
            };
            var refund = Refund.CreateAmountBasedRefundRequest(refundRequest, options);
            if (refund.Status == "success")
            {
                return refund;
            }
            else
            {
                _serviceErrorContainer.AddModelOnlyError(refund.ErrorMessage);
                return refund;
            }
        }
        public async Task<Cancel> CancelOrder(Order order,string description="")
        {
            CreateCancelRequest cancelRequest = new CreateCancelRequest()
            {
                Ip=GetIp(),
                PaymentId=order.PaymentResult.PaymentId,
                Description = description
            };
            var cancel=Cancel.Create(cancelRequest,options);
            if (cancel.Status == "success")
            {
                return cancel;
            }
            else
            {
                _serviceErrorContainer.AddModelOnlyError(cancel.ErrorMessage);
                return cancel;
            }
        }
        public string? GetIp()
        {
            var ip = HttpContext.Connection.RemoteIpAddress;
            var addressbytesString = "";
            int indexer = 1;
            if (ip == null)
                return null;
            var GetAddressBytes = ip.GetAddressBytes();
            GetAddressBytes.ToList().ForEach(x =>
            {
                if (GetAddressBytes.Length != indexer)
                    addressbytesString += $"{x}.";
                else
                    addressbytesString += $"{x}";
            });
            return addressbytesString;
        }
    }
}
