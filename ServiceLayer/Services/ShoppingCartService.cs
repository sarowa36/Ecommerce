using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Anonym.AnonymShoppingCartController;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace ServiceLayer.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IProductReadRepository _productReadRepository;
        readonly IShoppingCartItemReadRepository _shoppingCartItemReadRepository;
        readonly IShoppingCartItemWriteRepository _shoppingCartItemWriteRepository;
        readonly IServiceErrorContainer _serviceErrorContainer;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        private const string ShoppingCartCookieName = "shoppingCart";
        public ShoppingCartService(IHttpContextAccessor httpContextAccessor,
            IProductReadRepository productReadRepository,
            IShoppingCartItemWriteRepository shoppingCartItemWriteRepository,
            IShoppingCartItemReadRepository shoppingCartItemReadRepository,
            IServiceErrorContainer serviceResponseProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _productReadRepository = productReadRepository;
            _shoppingCartItemWriteRepository = shoppingCartItemWriteRepository;
            _shoppingCartItemReadRepository = shoppingCartItemReadRepository;
            _serviceErrorContainer = serviceResponseProvider;
        }
        public async Task AddOrUpdateOrRemoveProductToCookieAsync(int productId, int quantity)
        {
            Dictionary<int, int> cookieCart = new Dictionary<int, int>();
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                cookieCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookieCartString);
                if (cookieCart.ContainsKey(productId))
                {
                    if (quantity <= 0)
                    {
                        cookieCart.Remove(productId);
                    }
                    else
                    {
                        cookieCart[productId] = quantity;
                    }
                }
                else if (quantity > 0)
                {
                    cookieCart.Add(productId, quantity);
                }

            }
            else if (quantity > 0)
                cookieCart.Add(productId, quantity);
            else
            {
                _serviceErrorContainer.AddError("ModelOnly", "Quantity Must Be Greater Than 0");
            }
            HttpContext.Response.Cookies.Append(ShoppingCartCookieName, JsonConvert.SerializeObject(cookieCart));
        }
        public async Task<List<AnonymShoppingCartListValueVM>> GetListFromCookieAsync()
        {
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                Dictionary<int, int> cookieCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookieCartString);
                return _productReadRepository.GetAll().Where(x => cookieCart.Keys.Contains(x.Id))
                    .Select(x =>
                   new AnonymShoppingCartListValueVM
                   {
                       ProductId = x.Id,
                       ProductImage = x.Images[0],
                       ProductName = x.Name,
                       ProductPrice = x.Price,
                       Quantity = cookieCart[x.Id]
                   }).ToList();
            }
            else
            {
                return new List<AnonymShoppingCartListValueVM>();
            }
        }
        public async Task AddOrUpdateOrRemoveProductAsync(ApplicationUser user, int productId, int quantity)
        {

            if (_shoppingCartItemReadRepository.GetWhere(x => x.UserId == user.Id && x.ProductId == productId, out var item))
            {
                if (quantity <= 0)
                {
                    await _shoppingCartItemWriteRepository.DeleteAsync(item);
                }
                else
                {
                    item.Quantity = quantity;
                    await _shoppingCartItemWriteRepository.UpdateAsync(item);
                }
            }
            else if (quantity > 0)
            {
                await _shoppingCartItemWriteRepository.CreateAsync(new ShoppingCartItem() { ProductId = productId, Quantity = quantity, UserId = user.Id });
            }
            await _shoppingCartItemWriteRepository.SaveChangesAsync();

        }
        public async Task BulkAddItemAsync(ApplicationUser user, Dictionary<int, int> values)
        {
            if (values.Any(x => x.Value <= 0))
                _serviceErrorContainer.AddError("ModelOnly", "Quantity Must Be Greater Than 0");
            else if (_shoppingCartItemReadRepository.Exist(x => x.UserId == user.Id && values.Keys.Contains(x.ProductId)))
                _serviceErrorContainer.AddError("ModelOnly", "Item Already Exist");
            else
            {
                await _shoppingCartItemWriteRepository.CreateRangeAsync(values.Select(x => new ShoppingCartItem() { UserId = user.Id, ProductId = x.Key, Quantity = x.Value }));
                await _shoppingCartItemWriteRepository.SaveChangesAsync();
            }
        }
        public async Task CookieCartConvertToDbCartAsync(ApplicationUser user)
        {
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                Dictionary<int, int> cookieCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookieCartString);
                if (cookieCart != null && cookieCart.All(x => x.Key > 0 && x.Value > 0))
                {
                    var alreadyAddedValues = _shoppingCartItemReadRepository.GetAll().Where(x => x.UserId == user.Id && cookieCart.Keys.Contains(x.ProductId)).ToList();
                    var newValues = cookieCart.Keys.Except(alreadyAddedValues.Select(x => x.ProductId));
                    alreadyAddedValues.ForEach(x => x.Quantity = cookieCart[x.ProductId]);
                    await _shoppingCartItemWriteRepository.CreateRangeAsync(newValues.Select(x => new ShoppingCartItem() { UserId = user.Id, ProductId = x, Quantity = cookieCart[x] }));
                    await _shoppingCartItemWriteRepository.SaveChangesAsync();
                }
                HttpContext.Response.Cookies.Delete(ShoppingCartCookieName);
            }
        }
        public async Task<List<ShoppingCartItem>> GetListAsync(ApplicationUser user)
        {
            return _shoppingCartItemReadRepository.GetAll().Where(x => x.UserId == user.Id).Include(x => x.Product).ToList();
        }
        public async Task SetEmptyToCart(string userId)
        {
            await _shoppingCartItemWriteRepository.DeleteRangeAsync(_shoppingCartItemReadRepository.GetAll().Where(x => x.UserId == userId));
        }
    }
}

