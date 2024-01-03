using EntityLayer.Entities;
using EntityLayer.ViewModels.Anonym.AnonymShoppingCartController;

namespace ServiceLayer.Base.Services
{
    public interface IShoppingCartService
    {
        Task AddOrUpdateOrRemoveProductAsync(ApplicationUser user, int productId, int quantity);
        Task AddOrUpdateOrRemoveProductToCookieAsync(int productId, int quantity);
        Task BulkAddItemAsync(ApplicationUser user, Dictionary<int, int> values);
        Task CookieCartConvertToDbCartAsync(ApplicationUser user);
        Task<List<ShoppingCartItem>> GetListAsync(ApplicationUser user);
        Task<List<AnonymShoppingCartListValueVM>> GetListFromCookieAsync();
    }
}