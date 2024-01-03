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
        readonly IServiceErrorContainer _serviceResponseProvider;
        readonly IMapper _mapper;
        public ShoppingCartController(IShoppingCartService shoppingCartService, IServiceErrorContainer serviceResponseProvider, IIdentityService identityService, IMapper mapper)
        {
            _shoppingCartService = shoppingCartService;
            _serviceResponseProvider = serviceResponseProvider;
            _identityService = identityService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Write(int productId, int quantity)
        {
            var user =  _serviceResponseProvider.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
             _serviceResponseProvider.AddServiceResponse(()=>_shoppingCartService.AddOrUpdateOrRemoveProductAsync(user,productId, quantity));
            return _serviceResponseProvider.IsSuccess ? Ok() : BadRequest(_serviceResponseProvider.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var user = _serviceResponseProvider.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var response = _serviceResponseProvider.AddServiceResponse(()=>_shoppingCartService.GetListAsync(user));
            return _serviceResponseProvider.IsSuccess ? Ok( response.Select(x=>_mapper.Map<ShoppingCartItem,ShoppingCartItemValueVM>(x)).ToList()) : BadRequest(_serviceResponseProvider.Errors);
        }
    }
}
