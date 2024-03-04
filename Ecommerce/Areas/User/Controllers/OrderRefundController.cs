using AutoMapper;
using BusinessLayer.Validations.User.OrderRefundController;
using EntityLayer.DTOs.Areas.User.OrderRefundController;
using EntityLayer.ViewModels.User.OrderRefundController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.User.Controllers
{
    /// <summary>
    /// I shelved refund service for a while
    /// </summary>
    [Area("User")]
    [Authorize(Roles ="User")]
    [NonController]
    public class OrderRefundController : Controller
    {
        readonly IServiceErrorContainer _errorContainer;
        readonly IOrderRefundService _orderRefundService;
        readonly IIdentityService _identityService;
        readonly IMapper _mapper;
        readonly IServiceProvider _serviceProvider;
        public OrderRefundController(IServiceErrorContainer errorContainer, 
            IOrderRefundService orderRefundService, 
            IIdentityService identityService, 
            IMapper mapper, 
            IServiceProvider serviceProvider)
        {
            _errorContainer = errorContainer;
            _orderRefundService = orderRefundService;
            _identityService = identityService;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }
        [HttpPost]
        public async Task<IActionResult> CreateRefund(OrderRefundCreateDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<OrderRefundCreateDTOValidation>().Validate(model));
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var orderrefund=_errorContainer.AddServiceResponse(()=>_orderRefundService.CreateRefund(model,user.Id));
            return _errorContainer.IsSuccess ? Ok():BadRequest(_errorContainer.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var orderrefund = _errorContainer.AddServiceResponse(() => _orderRefundService.GetUserRefunds(user.Id));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<List<UserOrderRefundVM>>( orderrefund)) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> CancelRefund(int id)
        {
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            _errorContainer.AddServiceResponse(() => _orderRefundService.CancelRefund(id,user.Id));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
    }
}
