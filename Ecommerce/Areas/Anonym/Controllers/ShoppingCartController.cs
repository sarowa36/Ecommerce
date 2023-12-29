using AutoMapper;
using Azure;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.ShoppingCartController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.Anonym.Controllers
{
    [Area("Anonym")]
    public class ShoppingCartController : Controller
    {
        readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpPost]
        public async Task<IActionResult> Write(int productId, int quantity)
        {
            var response=await _shoppingCartService.AddOrUpdateOrRemoveProductToCookieAsync(productId,quantity);
            return response.IsSuccess ? Ok() : BadRequest(response.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var response = await _shoppingCartService.GetListFromCookieAsync();
            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Errors);
        }
    }
}
