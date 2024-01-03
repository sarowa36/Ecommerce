using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.Anonym.Controllers
{
    [Area("Anonym")]
    public class ShoppingCartController : Controller
    {
        readonly IShoppingCartService _shoppingCartService;
        readonly IServiceErrorContainer _serviceErrorProvider;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IServiceErrorContainer serviceErrorProvider)
        {
            _shoppingCartService = shoppingCartService;
            _serviceErrorProvider = serviceErrorProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Write(int productId, int quantity)
        {
            _serviceErrorProvider.AddServiceResponse(()=> _shoppingCartService.AddOrUpdateOrRemoveProductToCookieAsync(productId,quantity));
            return _serviceErrorProvider.IsSuccess ? Ok() : BadRequest(_serviceErrorProvider.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var response =_serviceErrorProvider.AddServiceResponse(()=>_shoppingCartService.GetListFromCookieAsync());
            return _serviceErrorProvider.IsSuccess ? Ok(response) : BadRequest(_serviceErrorProvider.Errors);
        }
    }
}
