using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using ServiceLayer.Services;

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
        public async Task<IActionResult> AddOrUpdate(int productId, int quantity, Dictionary<string, string> variation)
        {
            _serviceErrorProvider.AddServiceResponse(() => _shoppingCartService.AddOrUpdateToCookieAsync( productId, quantity, variation));
            return _serviceErrorProvider.IsSuccess ? Ok() : BadRequest(_serviceErrorProvider.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(string cartId, int quantity)
        {
            _serviceErrorProvider.AddServiceResponse(() => _shoppingCartService.UpdateCookieCartItemQuantity( cartId, quantity));
            return _serviceErrorProvider.IsSuccess ? Ok() : BadRequest(_serviceErrorProvider.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string cartId)
        {
            _serviceErrorProvider.AddServiceResponse(() => _shoppingCartService.DeleteCookieCartItem( cartId));
            return _serviceErrorProvider.IsSuccess ? Ok() : BadRequest(_serviceErrorProvider.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var response =_serviceErrorProvider.AddServiceResponse(()=>_shoppingCartService.GetListFromCookieAsync());
            return _serviceErrorProvider.IsSuccess ? Ok(response) : BadRequest(_serviceErrorProvider.Errors);
        }
    }
}
