using EntityLayer.Entities;
using EntityLayer.Enum;
using Iyzipay.Model;

namespace ServiceLayer.Base.Services
{
    public interface IOrderService
    {
        Task<Order> Accept(int orderId);
        Task<Order> CreateOrder(ApplicationUser user, List<ShoppingCartItem> cartItems, UserAddress adress, string targetName, string targetPhone);
        Task<Order> Delivered(int orderId);
        Task<Order> UpdateOrderFromPaymentCallback(CheckoutForm checkoutForm);
        Task UpdateOrderStatus(string token, OrderStatus orderStatus, object? requestValue);
        Task<List<Order>> GetAllOrders(OrderStatus? orderStatus, int? index = null, int? count = null);
        Task<int> GetAllOrdersCount();
        Task<int> GetUserOrderCount(ApplicationUser user);
        Task<List<Order>> GetUserOrders(ApplicationUser user, int? index = null, int? count = null);
        Task<Order> Ignore(int orderId);
        Task<Order> SendToCargo(int orderId, string cargoCode);
        Task<Order> CancelByUser(int orderId, string userId);
    }
}