using DataAccessLayer.Base.Repositories.OrderItemRepositories;
using DataAccessLayer.Base.Repositories.OrderRepositories;
using EntityLayer.Entities;
using EntityLayer.Enum;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using System.Drawing.Printing;

namespace ServiceLayer.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IServiceErrorContainer _serviceErrorContainer;
        readonly IOrderItemReadRepository _orderItemReadRepository;
        readonly IOrderItemWriteRepository _orderItemWriteRepository;
        public OrderService(IOrderWriteRepository orderWriteRepository,
            IOrderReadRepository orderReadRepository,
            IServiceErrorContainer serviceErrorContainer,
            IOrderItemReadRepository orderItemReadRepository,
            IOrderItemWriteRepository orderItemWriteRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _serviceErrorContainer = serviceErrorContainer;
            _orderItemReadRepository = orderItemReadRepository;
            _orderItemWriteRepository = orderItemWriteRepository;
        }
        public async Task<Order> CreateOrder(ApplicationUser user, List<ShoppingCartItem> cartItems, UserAddress adress, string targetName, string targetPhone)
        {
            if (cartItems != null && cartItems.Count > 0)
            {
                Order order = new()
                {
                    OrderItems = cartItems.Select(x => new OrderItem() { Price = x.Product.Price, 
                        Quantity = x.Quantity, 
                        ProductId = x.ProductId, 
                        Product = x.Product,
                        ProductImage=x.Product.Images.FirstOrDefault()??"",
                        ProductName=x.Product.Name }).ToList(),
                    TotalPrice = cartItems.Sum(x => x.Quantity * x.Product.Price),
                    PaidPrice = cartItems.Sum(x => x.Quantity * x.Product.Price),
                    User = user,
                    UserId = user.Id,
                    Address = adress,
                    TargetPhone = targetPhone,
                    TargetName = targetName,
                };
                await _orderWriteRepository.CreateAsync(order);
                await _orderWriteRepository.SaveChangesAsync();
                return order;
            }
            else
            {
                _serviceErrorContainer.AddError("ModelOnly", "Cart is empty");
                return null;
            }
        }
        public async Task<Order> UpdateOrderFromPaymentCallback(Iyzipay.Model.CheckoutForm checkoutForm)
        {
            if (_orderReadRepository.Get(int.Parse(checkoutForm.BasketId), out var order) && order.PaymentResult == null)
            {
                order.PaymentResult = checkoutForm;
                order.Token= checkoutForm.Token;
                if (checkoutForm.PaymentStatus == "SUCCESS" && checkoutForm.Status == Iyzipay.Model.Status.SUCCESS.ToString())
                    order.OrderStatus = EntityLayer.Enum.OrderStatus.WaitingApprove;
                else
                    order.OrderStatus = EntityLayer.Enum.OrderStatus.PaymentFail;
                await _orderWriteRepository.UpdateAsync(order);
                await _orderWriteRepository.SaveChangesAsync();
                return order;
            }
            else
            {
                _serviceErrorContainer.AddError("ModelOnly", "Timed out request");
                return null;
            }
        }
        public async Task UpdateOrderStatus(string token, OrderStatus orderStatus, object? requestValue)
        {
            if(_orderReadRepository.GetWhere(x=>x.Token== token, out var order))
            {
                order.OrderStatus = orderStatus;
                if(requestValue!=null)
                order.WebhookRequests.Add(requestValue);
                await _orderWriteRepository.UpdateAsync(order);
                await _orderWriteRepository.SaveChangesAsync();
            }
            else
            {
                _serviceErrorContainer.AddError("token", "There is no order with this token");
            }
        }
        public async Task<OrderRefund> CreateRefund(List<int> orderItemIds)
        {
            if (_orderItemReadRepository.GetAll().Where(x=>orderItemIds.Contains(x.Id)).All(x=>x.OrderRefundId==null && x.Order.OrderStatus==OrderStatus.Delivered))
            {
                OrderRefund orderRefund = new OrderRefund();
            }
            else
            {

            }
            return default;
        }
        public async Task<List<Order>> GetUserOrders(ApplicationUser user, int? index = null, int? count = null)
        {
            var query = _orderReadRepository.GetAll().Where(x => x.UserId == user.Id).Include(x => x.OrderItems).OrderByDescending(x => x.CreateDate).AsQueryable();
            if (index != null && count != null)
            {
                query = query.Skip((int)index).Take((int)count);
            }
            return query.ToList();
        }
        public async Task<int> GetUserOrderCount(ApplicationUser user)
        {
            return _orderReadRepository.GetAll().Where(x => x.UserId == user.Id).Count();
        }
        public async Task<List<Order>> GetAllOrders(OrderStatus? orderStatus, int? index = null, int? count = null)
        {
            var query = _orderReadRepository.GetAll().Include(x => x.OrderItems).OrderByDescending(x => x.CreateDate).AsQueryable();
            if (orderStatus != null) 
                query=query.Where(x=>x.OrderStatus == orderStatus);
            if (index != null && count != null)
                query = query.Skip((int)index).Take((int)count);
            return query.ToList();
        }
        public async Task<int> GetAllOrdersCount()
        {
            return _orderReadRepository.GetAll().Count();
        }
        public async Task<Order> Accept(int orderId)
        {
            if (_orderReadRepository.Exist(x => x.Id == orderId && x.OrderStatus == OrderStatus.WaitingApprove))
            {
                var order = _orderReadRepository.GetAll().Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderId && x.OrderStatus == OrderStatus.WaitingApprove);
                order.OrderStatus = OrderStatus.ApprovedAndPreparing;
                await _orderWriteRepository.UpdateAsync(order);
                await _orderWriteRepository.SaveChangesAsync();
                return order;
            }
            else
            {
                _serviceErrorContainer.AddError("ModelOnly", "Order Not Exist");
            }
            return null;
        }
        public async Task<Order> SendToCargo(int orderId, string cargoCode)
        {
            if (_orderReadRepository.Exist(x => x.Id == orderId && x.OrderStatus == OrderStatus.ApprovedAndPreparing))
            {
               
                if (!string.IsNullOrWhiteSpace(cargoCode) && cargoCode.Length > 3)
                {
                    var order = _orderReadRepository.GetAll().Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderId && x.OrderStatus == OrderStatus.ApprovedAndPreparing);
                    order.OrderStatus = OrderStatus.OnCargo;
                    order.CargoCode = cargoCode;
                    await _orderWriteRepository.UpdateAsync(order);
                    await _orderWriteRepository.SaveChangesAsync();
                    return order;
                }
                else
                    _serviceErrorContainer.AddError("cargoCode", "Cargo Code is not valid");
            }
            else
            {
                _serviceErrorContainer.AddError("ModelOnly", "Order Not Exist");
            }
            return null;
        }
        public async Task<Order> Delivered(int orderId)
        {
            if (_orderReadRepository.Exist(x => x.Id == orderId && x.OrderStatus == OrderStatus.OnCargo))
            {
                var order = _orderReadRepository.GetAll().Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderId && x.OrderStatus == OrderStatus.OnCargo);
                order.OrderStatus = OrderStatus.Delivered;
                await _orderWriteRepository.UpdateAsync(order);
                await _orderWriteRepository.SaveChangesAsync();
                return order;
            }
            else
            {
                _serviceErrorContainer.AddError("ModelOnly", "Order Not Exist");
            }
            return null;
        }
        public async Task<Order> Ignore(int orderId)
        {
            if (_orderReadRepository.Exist(x => x.Id == orderId))
            {
                var order = _orderReadRepository.GetAll().Include(x => x.OrderItems).FirstOrDefault(x => x.Id == orderId);
                order.OrderStatus = OrderStatus.Ignored;
                await _orderWriteRepository.UpdateAsync(order);
                await _orderWriteRepository.SaveChangesAsync();
                return order;
            }
            else
            {
                _serviceErrorContainer.AddError("ModelOnly", "Order Not Exist");
            }
            return null;
        }
    }
}
