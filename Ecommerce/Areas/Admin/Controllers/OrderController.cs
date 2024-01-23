using AutoMapper;
using EntityLayer.Enum;
using EntityLayer.ViewModels.IdentityController;
using EntityLayer.ViewModels.User.OrderController;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        readonly IOrderService _orderService;
        readonly IServiceErrorContainer _errorContainer;
        readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IServiceErrorContainer errorContainer, IMapper mapper)
        {
            _orderService = orderService;
            _errorContainer = errorContainer;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetList(OrderStatus? orderStatus,int index)
        {
            const int maxCount = 12;
            var orders =_errorContainer.AddServiceResponse(()=>_orderService.GetAllOrders(orderStatus, index, maxCount));
            var orderCount = _errorContainer.AddServiceResponse(() => _orderService.GetAllOrdersCount());
            return _errorContainer.IsSuccess ? Ok(new { values = _mapper.Map<List<UserOrderVM>>(orders), count = orderCount }) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Accept(int id)
        {
           var order= _errorContainer.AddServiceResponse(() => _orderService.Accept(id));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<UserOrderVM>(order)) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Ignore(int id)
        {

            var order = _errorContainer.AddServiceResponse(() => _orderService.Ignore(id));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<UserOrderVM>(order)) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> SendToCargo(int id,string cargoCode)
        {
            var order = _errorContainer.AddServiceResponse(() => _orderService.SendToCargo(id,cargoCode));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<UserOrderVM>(order)) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Delivered(int id)
        {
            var order = _errorContainer.AddServiceResponse(() => _orderService.Delivered(id));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<UserOrderVM>(order)) : BadRequest(_errorContainer.Errors);
        }
            
    }
}
