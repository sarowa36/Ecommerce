using AutoMapper;
using BusinessLayer.Validations.User.OrderRefundController;
using EntityLayer.DTOs.Areas.Admin.OrderRefundController;
using EntityLayer.ViewModels.Admin.OrderRefundController;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderRefundController : Controller
    {
        readonly IServiceErrorContainer _errorContainer;
        readonly IOrderRefundService _orderRefundService;
        readonly IServiceProvider _serviceProvider;
        readonly IMapper _mapper;
        readonly IIyziPayService _iyziPayService;
        public OrderRefundController(IServiceErrorContainer errorContainer, IOrderRefundService orderRefundService, IMapper mapper, IServiceProvider serviceProvider, IIyziPayService iyziPayService)
        {
            _errorContainer = errorContainer;
            _orderRefundService = orderRefundService;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _iyziPayService = iyziPayService;
        }
        public async Task<IActionResult> GetList()
        {
            return Ok(_mapper.Map<List<AdminOrderRefundVM>>( await _orderRefundService.GetAllRefunds()));
        }
        [HttpPost]
        public async Task<IActionResult> Ignore(int id)
        {
            _errorContainer.AddServiceResponse(()=>_orderRefundService.IgnoreRefund(id));
            return _errorContainer.IsSuccess? Ok():BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Approve(OrderRefundApproveDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<OrderRefundApproveDTOValidation>().Validate(model));
            _errorContainer.AddServiceResponse(() => _orderRefundService.ApproveRefund(model.Id, model.CargoCode));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
            var orderRefund= _errorContainer.AddServiceResponse(() => _orderRefundService.GetOrderRefund(id));
            var refund = _errorContainer.AddServiceResponse(() => _iyziPayService.RefundOrder(orderRefund));
            _errorContainer.AddServiceResponse(() => _orderRefundService.AcceptRefund(orderRefund,refund));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSellerMessage(int id,string message)
        {
            _errorContainer.AddServiceResponse(() => _orderRefundService.UpdateSellerMessage(id, message));
            return _errorContainer.IsSuccess ? Ok():BadRequest(_errorContainer.Errors);
        }

    }
}
