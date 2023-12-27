using AutoMapper;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.ShoppingCartController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ecommerce.Areas.Anonym.Controllers
{
    [Area("Anonym")]
    public class ShoppingCartController : Controller
    {
        private readonly IProductReadRepository _productReadRepository;
        private const string ShoppingCartCookieName = "shoppingCart";
        public ShoppingCartController(IProductReadRepository productReadRepository)
        {
            this._productReadRepository = productReadRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Write(int productId, int quantity)
        {
            Dictionary<int,int> cookieCart = new Dictionary<int, int>();
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString))
            {
                cookieCart = JsonConvert.DeserializeObject<Dictionary<int,int>>(cookieCartString);
                if(cookieCart.ContainsKey(productId))
                {
                    if(quantity<=0)
                    {
                        cookieCart.Remove(productId);
                    }
                    else
                    {
                        cookieCart[productId]= quantity;
                    }
                }
                else if(quantity>0)
                {
                    cookieCart.Add(productId, quantity);
                }
                
            }
            else if(quantity>0)
            {
                cookieCart.Add(productId,quantity);
            }
            HttpContext.Response.Cookies.Append(ShoppingCartCookieName, JsonConvert.SerializeObject(cookieCart));
            return Ok();
        }
        public async Task<IActionResult> GetList()
        {
            if (HttpContext.Request.Cookies.TryGetValue(ShoppingCartCookieName, out var cookieCartString)) { 
                Dictionary<int, int> cookieCart = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookieCartString);
                var values = _productReadRepository.GetAll().Where(x => cookieCart.Keys.Contains(x.Id))
                    .Select(x =>
                    new { ProductId = x.Id, 
                        ProductImage = x.Images[0],
                        ProductName=x.Name,
                        ProductPrice = x.Price, 
                        Quantity = cookieCart[x.Id] }).ToList();
             return Ok(values);
            }
            return Ok();
        }
    }
}
