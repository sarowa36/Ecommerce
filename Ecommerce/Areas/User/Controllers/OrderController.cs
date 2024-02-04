using AutoMapper;
using EntityLayer.ViewModels.User.OrderController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using ServiceLayer.Services;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles ="User")]
    public class OrderController : Controller
    {
        readonly IOrderService _orderService;
        readonly IServiceErrorContainer _errorContainer;
        readonly IIdentityService _identityService;
        readonly IMapper _mapper;
        public OrderController(IOrderService orderService,
            IIdentityService identityService,
            IServiceErrorContainer errorContainer,
            IMapper mapper)
        {
            _orderService = orderService;
            _identityService = identityService;
            _errorContainer = errorContainer;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetList(int index)
        {
            const int maxCount = 12;
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var orders = _errorContainer.AddServiceResponse(() => _orderService.GetUserOrders(user,index, maxCount));
            var orderCount = _errorContainer.AddServiceResponse(() => _orderService.GetUserOrderCount(user));
            return _errorContainer.IsSuccess ? Ok(new { values=_mapper.Map<List<UserOrderVM>>(orders),count=orderCount }) : BadRequest(_errorContainer.Errors);
        }
    }
}
