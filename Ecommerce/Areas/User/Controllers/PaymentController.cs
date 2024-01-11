using BusinessLayer.Validations.User.PaymentController;
using EntityLayer.ViewModels.User.PaymentController;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using ServiceLayer.Services;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]

    public class PaymentController : Controller
    {
        readonly IIyziPayService _paymentService;
        readonly IShoppingCartService _shoppingCartService;
        readonly IIdentityService _identityService;
        readonly IServiceErrorContainer _serviceErrorContainer;
        readonly IOrderService _orderService;
        readonly IServiceProvider _serviceProvider;
        readonly IUserAddressService _userAddressService;
        public PaymentController(IIyziPayService paymentService,
            IShoppingCartService shoppingCartService,
            IIdentityService identityService,
            IServiceErrorContainer serviceErrorContainer,
            OrderService orderService,
            IUserAddressService userAddressService,
            IServiceProvider serviceProvider)
        {
            _paymentService = paymentService;
            _shoppingCartService = shoppingCartService;
            _identityService = identityService;
            _serviceErrorContainer = serviceErrorContainer;
            _orderService = orderService;
            _userAddressService = userAddressService;
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> StartPayment(StartPaymentVM model)
        {
            _serviceErrorContainer.BindValidation(_serviceProvider.GetService<StartPaymentVMValidation>().Validate(model));
            var user = _serviceErrorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var shoppingCartItems = _serviceErrorContainer.AddServiceResponse(() => _shoppingCartService.GetListAsync(user));
            var address = _serviceErrorContainer.AddServiceResponse(() => _userAddressService.Get(user, model.SelectedAddressId));
            var order = _serviceErrorContainer.AddServiceResponse(() => _orderService.CreateOrder(user, shoppingCartItems, address, model.TargetName, model.TargetPhone));
            var paymentRequest = _serviceErrorContainer.AddServiceResponse(() => _paymentService.StartPayment(order,user));
            return _serviceErrorContainer.IsSuccess ? Ok(new { redirect = paymentRequest.PaymentPageUrl }) : BadRequest(_serviceErrorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Callback(RetrieveCheckoutFormRequest val)
        {
            var result = _serviceErrorContainer.AddServiceResponse(() => _paymentService.ProcessPayment(val));
            var order = _serviceErrorContainer.AddServiceResponse(() => _orderService.FinishOrder(result));
            _serviceErrorContainer.AddServiceResponse(() => _shoppingCartService.SetEmptyToCart(order.UserId));
            if (_serviceErrorContainer.IsSuccess)
            {
                return Redirect("/User/Orders?CurrentOrder=" + result.BasketId);
            }
            else
            {
                return Content(String.Join("<br>", _serviceErrorContainer.Errors.Select(x => x.Key + " => " + x.Value + "\n")), "text/html");
            }
        }
    }
}
