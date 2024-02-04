using EntityLayer.Entities;
using EntityLayer.ViewModels.Anonym.AnonymShoppingCartController;

namespace ServiceLayer.Base.Services
{
    public interface IShoppingCartService
    {
        Task AddOrUpdateToCookieAsync(int productId, int quantity, Dictionary<string, string> variaton);
        Task UpdateCookieCartItemQuantity(string cartId, int quantity);
        Task DeleteCookieCartItem(string cartId);
        Task CookieCartConvertToDbCartAsync(ApplicationUser user);
        Task AddOrUpdateAsync(ApplicationUser user, int productId, int quantity, Dictionary<string, string> variaton);
        Task UpdateCartItemQuantity(ApplicationUser user, int cartId, int quantity);
        Task DeleteCartItem(ApplicationUser user, int id);

        Task<List<ShoppingCartItem>> GetListAsync(ApplicationUser user);
        Task<List<AnonymShoppingCartListValueVM>> GetListFromCookieAsync();
        Task SetEmptyToCart(string userId);
    }
}