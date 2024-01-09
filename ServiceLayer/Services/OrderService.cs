using DataAccessLayer.Base.Repositories.OrderRepositories;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace ServiceLayer.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderReadRepository _orderReadRepository;
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IServiceErrorContainer _serviceErrorContainer;

        public OrderService(IOrderWriteRepository orderWriteRepository,
            IOrderReadRepository orderReadRepository,
            IServiceErrorContainer serviceErrorContainer)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _serviceErrorContainer = serviceErrorContainer;
        }
        public async Task<Order> CreateOrder(ApplicationUser user, List<ShoppingCartItem> cartItems, UserAddress adress,string targetName,string targetPhone)
        {
            if (cartItems != null && cartItems.Count > 0)
            {
                Order order = new Order()
                {
                    OrderItems = cartItems.Select(x => new OrderItem() { Price = x.Product.Price, Quantity = x.Quantity, ProductId = x.ProductId, Product = x.Product }).ToList(),
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
        public async Task<Order> FinishOrder(Iyzipay.Model.CheckoutForm checkoutForm)
        {
            if (_orderReadRepository.Get(int.Parse(checkoutForm.BasketId), out var order) && order.PaymentResult == null)
            {
                order.PaymentResult = checkoutForm;
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
        public async Task<List<Order>> GetUserOrders(ApplicationUser user, int? index = null, int? count = null)
        {
            var query = _orderReadRepository.GetAll().Where(x => x.UserId == user.Id).Include(x => x.OrderItems).ThenInclude(x => x.Product).OrderByDescending(x => x.CreateDate).AsQueryable();
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
    }
}
