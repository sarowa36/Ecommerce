using AutoMapper;
using DataAccessLayer;
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
        readonly IIyziPayService _payService;
        readonly IMapper _mapper;
        readonly ADC _db;
        public OrderController(IOrderService orderService,
            IIdentityService identityService,
            IServiceErrorContainer errorContainer,
            IMapper mapper,
            IIyziPayService payService,
            ADC db)
        {
            _orderService = orderService;
            _identityService = identityService;
            _errorContainer = errorContainer;
            _mapper = mapper;
            _payService = payService;
            _db = db;
        }
        public async Task<IActionResult> GetList(int index)
        {
            const int maxCount = 12;
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var orders = _errorContainer.AddServiceResponse(() => _orderService.GetUserOrders(user,index, maxCount));
            var orderCount = _errorContainer.AddServiceResponse(() => _orderService.GetUserOrderCount(user));
            return _errorContainer.IsSuccess ? Ok(new { values=_mapper.Map<List<UserOrderVM>>(orders),count=orderCount }) : BadRequest(_errorContainer.Errors);
        }
        public async Task<IActionResult> Cancel(int orderId)
        {
            using var transaction = _db.Database.BeginTransaction();
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var order=_errorContainer.AddServiceResponse(() => _orderService.CancelByUser(orderId, user.Id));
            _errorContainer.AddServiceResponse(() => DateTime.Now - order.CreateDate < TimeSpan.FromHours(24) ? _payService.CancelOrder(order,"User based cancellation request") : _payService.RefundOrder(order));
            _errorContainer.AddServiceResponse(() => transaction.CommitAsync());
            return _errorContainer.IsSuccess ? Ok():BadRequest(_errorContainer.Errors);
        }
    }
}
