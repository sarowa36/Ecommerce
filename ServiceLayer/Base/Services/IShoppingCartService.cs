using EntityLayer.Entities;
using ServiceLayer.ServiceResults.AnonymShoppingCartService;
using ServiceLayer.ServiceResults.ShoppingCartService;

namespace ServiceLayer.Base.Services
{
    public interface IShoppingCartService
    {
        Task<AddOrUpdateOrRemoveProductResponse> AddOrUpdateOrRemoveProductAsync(int productId, int quantity);
        Task<AddOrUpdateOrRemoveProductToCookieResponse> AddOrUpdateOrRemoveProductToCookieAsync(int productId, int quantity);
        Task<BulkAddItemResponse> BulkAddItemAsync(Dictionary<int, int> values);
        Task<GetListResponse> GetListAsync();
        Task<CookieCartConvertToDbCartResponse> CookieCartConvertToDbCartAsync(ApplicationUser user);
        Task<GetListFromCookieResponse> GetListFromCookieAsync();
    }
}