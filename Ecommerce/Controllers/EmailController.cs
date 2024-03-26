using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base.Services;

namespace Ecommerce.Controllers
{
    public class EmailController : Controller
    {
        readonly IOrderService _orderService;
        readonly IIdentityService _identityService;
        public EmailController(IOrderService orderService, IIdentityService identityService)
        {
            _orderService = orderService;
            _identityService = identityService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmationEmail(string token,string userId) {
            ViewData["token"] = token;
            ViewData["userId"] = userId;
            return View(); 
        }
        public async Task<IActionResult> OrderEmail(int id)
        {
            return View(await _orderService.GetOrder(id));
        }
        public async Task<IActionResult> ResetPasswordEmail(string token,string userId)
        {
            ViewData["token"] = token;
            ViewData["userId"] = userId;
            return View();
        }
    }
}
