using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Anonym.AnonymShoppingCartController;
using EntityLayer.ViewModels.User.ShoppingCartController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.Base.Services;
using ServiceLayer.ServiceResults.AnonymShoppingCartService;
using ServiceLayer.ServiceResults.ShoppingCartService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IProductReadRepository _productReadRepository;
        readonly IShoppingCartItemReadRepository _shoppingCartItemReadRepository;
        readonly IShoppingCartItemWriteRepository _shoppingCartItemWriteRepository;
        readonly IIdentityService _identityService;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        private const string ShoppingCartCookieName = "shoppingCart";
        public ShoppingCartService(IHttpContextAccessor httpContextAccessor, IProductReadRepository productReadRepository, IShoppingCartItemWriteRepository shoppingCartItemWriteRepository, IShoppingCartItemReadRepository shoppingCartItemReadRepository, IIdentityService identityService)
        {
            _httpContextAccessor = httpContextAccessor;
            _productReadRepository = productReadRepository;
            _shoppingCartItemWriteRepository = shoppingCartItemWriteRepository;
            _shoppingCartItemReadRepository = shoppingCartItemReadRepository;
            _identityService = identityService;
        }
        public async Task<AddOrUpdateOrRemoveProductToCookieResponse> AddOrUpdateOrRemoveProductToCookieAsync(int productId, int quantity)
        {
            var response = new AddOrUpdateOrRemoveProductToCookieResponse();
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
                response.Errors.Add("ModelOnly", "Quantity Must Be Greater Than 0");
                return response;
            }
            HttpContext.Response.Cookies.Append(ShoppingCartCookieName, JsonConvert.SerializeObject(cookieCart));
            return response;
        }
        public async Task<GetListFromCookieResponse> GetListFromCookieAsync()
        {
            var response = new GetListFromCookieResponse();
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                Dictionary<int, int> cookieCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookieCartString);
                response.Value = _productReadRepository.GetAll().Where(x => cookieCart.Keys.Contains(x.Id))
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
            return response;
        }
        public async Task<AddOrUpdateOrRemoveProductResponse> AddOrUpdateOrRemoveProductAsync(int productId, int quantity)
        {
            var response = new AddOrUpdateOrRemoveProductResponse();
            var getCurrentUserResponse = await _identityService.GetCurrentUserAsync();
            response.BindResponse(getCurrentUserResponse);
            if (response.IsSuccess)
            {
                var user = getCurrentUserResponse.Value;
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
            return response;
        }
        public async Task<BulkAddItemResponse> BulkAddItemAsync(Dictionary<int, int> values)
        {
            var response = new BulkAddItemResponse();
            var getCurrentUserResponse = await _identityService.GetCurrentUserAsync();
            response.BindResponse(getCurrentUserResponse);
            if (values.Any(x => x.Value <= 0))
                response.Errors.Add("ModelOnly", "Quantity Must Be Greater Than 0");
            if (getCurrentUserResponse.IsSuccess && _shoppingCartItemReadRepository.Exist(x => x.UserId == getCurrentUserResponse.Value.Id && values.Keys.Contains(x.ProductId)))
                response.Errors.Add("ModelOnly", "Item Already Exist");
            if (response.IsSuccess)
            {
                var user = getCurrentUserResponse.Value;
                await _shoppingCartItemWriteRepository.CreateRangeAsync(values.Select(x => new ShoppingCartItem() { UserId = user.Id, ProductId = x.Key, Quantity = x.Value }));
                await _shoppingCartItemWriteRepository.SaveChangesAsync();
            }
            return response;
        }
        public async Task<CookieCartConvertToDbCartResponse> CookieCartConvertToDbCartAsync(ApplicationUser user)
        {
            var response = new CookieCartConvertToDbCartResponse();
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                Dictionary<int, int> cookieCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookieCartString);
                if(cookieCart!= null && cookieCart.All(x=>x.Key>0 && x.Value>0))
                {
                    var alreadyAddedValues = _shoppingCartItemReadRepository.GetAll().Where(x => x.UserId == user.Id && cookieCart.Keys.Contains(x.ProductId)).ToList();
                    var newValues=cookieCart.Keys.Except(alreadyAddedValues.Select(x=>x.ProductId));
                    alreadyAddedValues.ForEach(x => x.Quantity = cookieCart[x.ProductId]);
                    await _shoppingCartItemWriteRepository.CreateRangeAsync(newValues.Select(x=>new ShoppingCartItem() { UserId=user.Id, ProductId = x, Quantity = cookieCart[x] }));
                    await _shoppingCartItemWriteRepository.SaveChangesAsync();
                }
                HttpContext.Response.Cookies.Delete(ShoppingCartCookieName);
            }
            return response;
        }
        public async Task<GetListResponse> GetListAsync()
        {
            var response = new GetListResponse();
            var getCurrentUserResponse = await _identityService.GetCurrentUserAsync();
            response.BindResponse(getCurrentUserResponse);
            if (response.IsSuccess)
            {
                var user = getCurrentUserResponse.Value;
                response.Value = _shoppingCartItemReadRepository.GetAll().Where(x => x.UserId == user.Id).Include(x => x.Product).ToList();
            }
            return response;
        }
    }
}

