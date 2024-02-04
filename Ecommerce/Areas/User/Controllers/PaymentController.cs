using BusinessLayer.Validations.User.PaymentController;
using EntityLayer.DTOs.Areas.User.PaymentController;
using EntityLayer.DTOs.Integrations.Iyzipay;
using EntityLayer.Enum;
using Iyzipay.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using System.Text;
using ToolsLayer.Http;

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
        readonly IWebHostEnvironment _webHostEnvironment;
        public PaymentController(IIyziPayService paymentService,
            IShoppingCartService shoppingCartService,
            IIdentityService identityService,
            IServiceErrorContainer serviceErrorContainer,
            IOrderService orderService,
            IUserAddressService userAddressService,
            IServiceProvider serviceProvider,
            IWebHostEnvironment webHostEnvironment)
        {
            _paymentService = paymentService;
            _shoppingCartService = shoppingCartService;
            _identityService = identityService;
            _serviceErrorContainer = serviceErrorContainer;
            _orderService = orderService;
            _userAddressService = userAddressService;
            _serviceProvider = serviceProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> StartPayment(StartPaymentDTO model)
        {
            _serviceErrorContainer.BindValidation(_serviceProvider.GetService<StartPaymentDTOValidation>().Validate(model));
            var user = _serviceErrorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var shoppingCartItems = _serviceErrorContainer.AddServiceResponse(() => _shoppingCartService.GetListAsync(user));
            var address = _serviceErrorContainer.AddServiceResponse(() => _userAddressService.Get(user, model.SelectedAddressId));
            var order = _serviceErrorContainer.AddServiceResponse(() => _orderService.CreateOrder(user, shoppingCartItems, address, model.TargetName, model.TargetPhone));
            var paymentRequest = _serviceErrorContainer.AddServiceResponse(() => _paymentService.StartPayment(order, user));
            return _serviceErrorContainer.IsSuccess ? Ok(new { redirect = paymentRequest.PaymentPageUrl }) : BadRequest(_serviceErrorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Callback(RetrieveCheckoutFormRequest val)
        {
            var result = _serviceErrorContainer.AddServiceResponse(() => _paymentService.ProcessPayment(val));
            var order = _serviceErrorContainer.AddServiceResponse(() => _orderService.UpdateOrderFromPaymentCallback(result));
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
        [HttpPost]
        public async Task<IActionResult> WebHook([FromBody] IyziWebhookDTO model)
        {
            var requestObject = await HttpContext.Request.GetAsSeriableObjectAsync(model);
            _serviceErrorContainer.AddServiceResponse(() =>
                _orderService.UpdateOrderStatus(
                    model.Token,
                    model.Status == "SUCCESS" ? OrderStatus.WaitingApprove : OrderStatus.PaymentFail, 
                    requestObject)
                    );
            

            return Ok();
        }
    }
}
