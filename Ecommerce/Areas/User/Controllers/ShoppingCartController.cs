using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.ShoppingCartController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class ShoppingCartController : Controller
    {
        readonly IShoppingCartService _shoppingCartService;
        readonly IIdentityService _identityService;
        readonly IServiceErrorContainer _serviceErrorProvider;
        readonly IMapper _mapper;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IServiceErrorContainer serviceErrorProvider, IIdentityService identityService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _serviceErrorProvider = serviceErrorProvider;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(int productId, int quantity,Dictionary<string,string> variation)
        {
            var user =  _serviceErrorProvider.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
             _serviceErrorProvider.AddServiceResponse(()=>_shoppingCartService.AddOrUpdateAsync(user,productId, quantity,variation));
            return _serviceErrorProvider.IsSuccess ? Ok() : BadRequest(_serviceErrorProvider.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartId, int quantity)
        {
            var user = _serviceErrorProvider.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            _serviceErrorProvider.AddServiceResponse(() => _shoppingCartService.UpdateCartItemQuantity(user,cartId, quantity));
            return _serviceErrorProvider.IsSuccess ? Ok() : BadRequest(_serviceErrorProvider.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int cartId)
        {
            var user = _serviceErrorProvider.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            _serviceErrorProvider.AddServiceResponse(() => _shoppingCartService.DeleteCartItem(user, cartId));
            return _serviceErrorProvider.IsSuccess ? Ok() : BadRequest(_serviceErrorProvider.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var user = _serviceErrorProvider.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var response = _serviceErrorProvider.AddServiceResponse(()=>_shoppingCartService.GetListAsync(user));
            return _serviceErrorProvider.IsSuccess ? Ok( response.Select(x=>_mapper.Map<ShoppingCartItem,ShoppingCartItemValueVM>(x)).ToList()) : BadRequest(_serviceErrorProvider.Errors);
        }
    }
}
