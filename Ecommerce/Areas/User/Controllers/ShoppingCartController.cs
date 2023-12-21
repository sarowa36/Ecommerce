using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles ="User")]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartItemWriteRepository _shoppingCartItemWriteRepository;
        private readonly IShoppingCartItemReadRepository _shoppingCartItemReadRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(IShoppingCartItemWriteRepository shoppingCartItemWriteRepository, IShoppingCartItemReadRepository shoppingCartItemReadRepository, UserManager<ApplicationUser> userManager)
        {
            _shoppingCartItemWriteRepository = shoppingCartItemWriteRepository;
            _shoppingCartItemReadRepository = shoppingCartItemReadRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Write(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if(_shoppingCartItemReadRepository.GetWhere(x=>x.UserId==user.Id && x.ProductId==productId,out var item))
            {
                if(quantity<=0)
                {
                    await _shoppingCartItemWriteRepository.DeleteAsync(item);
                }
                else
                {
                    item.Quantity = quantity;
                    await _shoppingCartItemWriteRepository.UpdateAsync(item);
                }
            }
            else if(quantity>0) 
            {
                await _shoppingCartItemWriteRepository.CreateAsync(new ShoppingCartItem() { ProductId=productId,Quantity=quantity,UserId=user.Id});
            }
            await _shoppingCartItemWriteRepository.SaveChangesAsync();
            return Ok();
        }
    }
}
