using EntityLayer.Entities;
using Iyzipay.Model;

namespace ServiceLayer.Base.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(ApplicationUser user, List<ShoppingCartItem> cartItems,UserAddress adress, string targetName, string targetPhone);
        Task<Order> FinishOrder(CheckoutForm checkoutForm);
        Task<int> GetUserOrderCount(ApplicationUser user);
        Task<List<Order>> GetUserOrders(ApplicationUser user, int? index = null, int? count = null);
    }
}