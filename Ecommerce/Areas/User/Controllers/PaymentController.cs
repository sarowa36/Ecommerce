using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    public class PaymentController : Controller
    {
        readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Accept()
        {
            //var result =await _paymentService.StartPayment();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Callback(RetrieveCheckoutFormRequest val)
        {
            var result = await _paymentService.RetrivePayment(val);
            return Ok(result);
        }
    }
}
