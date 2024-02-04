using AutoMapper;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using EntityLayer.Entities.JsonCookieEntities;
using EntityLayer.ViewModels.Anonym.AnonymShoppingCartController;
using EntityLayer.ViewModels.CommonVM;
using Iyzipay.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using System.Security.Cryptography;
using ToolsLayer.TripleDESCryption;

namespace ServiceLayer.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IProductReadRepository _productReadRepository;
        readonly IShoppingCartItemReadRepository _shoppingCartItemReadRepository;
        readonly IShoppingCartItemWriteRepository _shoppingCartItemWriteRepository;
        readonly IServiceErrorContainer _serviceErrorContainer;
        readonly IMapper _mapper;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        private const string ShoppingCartCookieName = "shoppingCart";
        public ShoppingCartService(IHttpContextAccessor httpContextAccessor,
            IProductReadRepository productReadRepository,
            IShoppingCartItemWriteRepository shoppingCartItemWriteRepository,
            IShoppingCartItemReadRepository shoppingCartItemReadRepository,
            IServiceErrorContainer serviceResponseProvider,
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _productReadRepository = productReadRepository;
            _shoppingCartItemWriteRepository = shoppingCartItemWriteRepository;
            _shoppingCartItemReadRepository = shoppingCartItemReadRepository;
            _serviceErrorContainer = serviceResponseProvider;
            _mapper = mapper;
        }
        public async Task AddOrUpdateToCookieAsync(int productId, int quantity, Dictionary<string, string> variaton)
        {

            List<CookieCartValue> cookieCart = new List<CookieCartValue>();
            var product = _productReadRepository.Get(productId);
            #region Validation
            // Controling variation is exist and acceptable
            if (!product.Variation.All(
                x => variaton.ContainsKey(x.Id) && x.Value.Contains(variaton[x.Id]))
                ||
                !variaton.All(
                    x => product.Variation.Any(
                        y => y.Id == x.Key && y.Value.Contains(x.Value)
                        )
                    ))
            {
                _serviceErrorContainer.AddModelOnlyError("Please select all variations");
                return;
            }
            if (quantity <= 0)
            {
                _serviceErrorContainer.AddError("ModelOnly", "Quantity Must Be Greater Than 0");
                return;
            }
            #endregion
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                cookieCart = JsonConvert.DeserializeObject<List<CookieCartValue>>(ToolsLayer.TripleDESCryption.TripleDES.Decrypt(cookieCartString));
                var cookieCartValue = cookieCart.FirstOrDefault(x => x.ProductId == productId && x.Variation.Select(y => new KeyValuePair<string, string>(y.Id, y.Value)).OrderBy(y => y.Key).SequenceEqual(variaton.OrderBy(y => y.Key)));
                if (cookieCartValue != null)
                    cookieCartValue.Quantity += quantity;
                else
                    cookieCart.Add(new CookieCartValue() { ProductId = productId, Quantity = quantity, Variation = variaton.Select(x=>new CookieSelectedProductVariation() { Id=x.Key,Value=x.Value,Name=product.Variation.FirstOrDefault(y=>y.Id==x.Key).Name}).ToList() });
            }
            else
            {
                cookieCart.Add(new CookieCartValue() { ProductId = productId, Quantity = quantity, Variation = variaton.Select(x => new CookieSelectedProductVariation() { Id = x.Key, Value = x.Value, Name = product.Variation.FirstOrDefault(y => y.Id == x.Key).Name }).ToList() });
            }
            HttpContext.Response.Cookies.Append(ShoppingCartCookieName, ToolsLayer.TripleDESCryption.TripleDES.Encrypt(JsonConvert.SerializeObject(cookieCart)));
        }
        public async Task UpdateCookieCartItemQuantity(string cartId, int quantity)
        {
            List<CookieCartValue> cookieCart = new List<CookieCartValue>();
            if (quantity <= 0)
            {
                _serviceErrorContainer.AddModelOnlyError("Quantity is unacceptable");
                return;
            }
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                cookieCart = JsonConvert.DeserializeObject<List<CookieCartValue>>(ToolsLayer.TripleDESCryption.TripleDES.Decrypt(cookieCartString));
                var cartVal = cookieCart.FirstOrDefault(x => x.Id == cartId);
                if (cartVal == null)
                {
                    _serviceErrorContainer.AddModelOnlyError("Cart item doesnt exist");
                    return;
                }
                cartVal.Quantity = quantity;
            }

            HttpContext.Response.Cookies.Append(ShoppingCartCookieName, ToolsLayer.TripleDESCryption.TripleDES.Encrypt(JsonConvert.SerializeObject(cookieCart)));
        }
        public async Task DeleteCookieCartItem(string cartId)
        {
            List<CookieCartValue> cookieCart = new List<CookieCartValue>();
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                cookieCart = JsonConvert.DeserializeObject<List<CookieCartValue>>(ToolsLayer.TripleDESCryption.TripleDES.Decrypt(cookieCartString));
                var cartVal = cookieCart.FirstOrDefault(x => x.Id == cartId);
                if (cartVal == null)
                {
                    _serviceErrorContainer.AddModelOnlyError("Cart item doesnt exist");
                    return;
                }
                cookieCart.Remove(cartVal);
            }
            HttpContext.Response.Cookies.Append(ShoppingCartCookieName, ToolsLayer.TripleDESCryption.TripleDES.Encrypt(JsonConvert.SerializeObject(cookieCart)));
        }
        public async Task<List<AnonymShoppingCartListValueVM>> GetListFromCookieAsync()
        {
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                List<CookieCartValue> cookieCart = JsonConvert.DeserializeObject<List<CookieCartValue>>(ToolsLayer.TripleDESCryption.TripleDES.Decrypt(cookieCartString));
                var products = _productReadRepository.GetAll().Where(x => cookieCart.Select(y=>y.ProductId).Contains(x.Id)).ToList();
                var returnValue = new List<AnonymShoppingCartListValueVM>();
                cookieCart.ForEach(cookieVal =>
                {
                    var product = products.FirstOrDefault(prod => prod.Id == cookieVal.ProductId);
                    returnValue.Add(new AnonymShoppingCartListValueVM()
                    {
                        Id = cookieVal.Id,
                        ProductId = product.Id,
                        ProductImage = product.Images[0],
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        Quantity = cookieVal.Quantity,
                        SelectedVariation = _mapper.Map<List<SelectedProductVariationVM>>(cookieVal.Variation)
                    });
                });
                return returnValue;
            }
            else
            {
                return new List<AnonymShoppingCartListValueVM>();
            }
        }
        public async Task AddOrUpdateAsync(ApplicationUser user, int productId, int quantity, Dictionary<string, string> variaton)
        {
            if (quantity <= 0)
            {
                _serviceErrorContainer.AddError("ModelOnly", "Quantity Must Be Greater Than 0");
                return;
            }
            var item = _shoppingCartItemReadRepository.GetAll().Where(x => x.UserId == user.Id &&
            x.ProductId == productId).ToList().FirstOrDefault(x =>
            x.Variation.Select(y=>new KeyValuePair<string,string>(y.Id,y.Value)).OrderBy(x => x.Key).SequenceEqual(variaton.OrderBy(y => y.Key))
            );
            if (item != null)
            {
                if (quantity >= 0)
                {
                    item.Quantity = quantity;
                    await _shoppingCartItemWriteRepository.UpdateAsync(item);
                }
            }
            else if (quantity > 0)
            {
                var product = _productReadRepository.Get(productId);
                // Controling variation is exist and acceptable
                if (!product.Variation.All(
                x => variaton.ContainsKey(x.Id) && x.Value.Contains(variaton[x.Id]))
                ||
                !variaton.All(
                    x => product.Variation.Any(
                        y => y.Id == x.Key && y.Value.Contains(x.Value)
                        )
                    )
                )
                {
                    _serviceErrorContainer.AddModelOnlyError("Please select all variations");
                    return;
                }
                await _shoppingCartItemWriteRepository.CreateAsync(new ShoppingCartItem() { ProductId = productId, Quantity = quantity, UserId = user.Id, Variation = variaton.Select(x=>new SelectedProductVariation() { Id=x.Key,Name=product.Variation.FirstOrDefault(y=>y.Id==x.Key).Name,Value=x.Value}).ToList() });
            }
            await _shoppingCartItemWriteRepository.SaveChangesAsync();

        }
        public async Task UpdateCartItemQuantity(ApplicationUser user, int cartId, int quantity)
        {
            if (quantity <= 0)
            {
                _serviceErrorContainer.AddModelOnlyError("Quantity is unacceptable");
                return;
            }
            var cartVal = _shoppingCartItemReadRepository.GetWhere(x => x.UserId == user.Id && x.Id == cartId);
            if (cartVal == null)
            {
                _serviceErrorContainer.AddModelOnlyError("Cart item doesnt exist");
                return;
            }
            cartVal.Quantity = quantity;
            await _shoppingCartItemWriteRepository.UpdateAsync(cartVal);
            await _shoppingCartItemWriteRepository.SaveChangesAsync();
        }
        public async Task DeleteCartItem(ApplicationUser user, int id)
        {
            var cartVal = _shoppingCartItemReadRepository.GetWhere(x => x.UserId == user.Id && x.Id == id);
            if (cartVal == null)
            {
                _serviceErrorContainer.AddModelOnlyError("Cart item doesnt exist");
                return;
            }
            await _shoppingCartItemWriteRepository.DeleteAsync(cartVal);
            await _shoppingCartItemWriteRepository.SaveChangesAsync();
        }
        public async Task CookieCartConvertToDbCartAsync(ApplicationUser user)
        {
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                List<CookieCartValue> cookieCart = JsonConvert.DeserializeObject<List<CookieCartValue>>(ToolsLayer.TripleDESCryption.TripleDES.Decrypt(cookieCartString));
                if (cookieCart != null && cookieCart.Count > 0 && cookieCart.All(x => x.Quantity > 0))
                {
                    var cartItemList = _shoppingCartItemReadRepository.GetAll().Where(x => x.UserId == user.Id && cookieCart.Select(y=>y.ProductId).Contains(x.ProductId)).ToList();
                    var newValues = new List<ShoppingCartItem>();
                    foreach (var cookieCartItem in cookieCart)
                    {
                        var cartItem = cartItemList.FirstOrDefault(x => cookieCartItem.ProductId == x.ProductId && x.Variation.Select(y => new KeyValuePair<string, string>(y.Id, y.Value)).OrderBy(y => y.Key).SequenceEqual(cookieCartItem.Variation.Select(y => new KeyValuePair<string, string>(y.Id, y.Value)).OrderBy(x => x.Key)));
                        if (cartItem == null)
                            newValues.Add(new ShoppingCartItem() { ProductId = cookieCartItem.ProductId, Quantity = cookieCartItem.Quantity, Variation = _mapper.Map<List<SelectedProductVariation>>(cookieCartItem.Variation), UserId = user.Id });
                        else
                            cartItem.Quantity = cookieCartItem.Quantity;
                    }
                    await _shoppingCartItemWriteRepository.CreateRangeAsync(newValues);
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
            await _shoppingCartItemWriteRepository.SaveChangesAsync();
        }
    }
}

