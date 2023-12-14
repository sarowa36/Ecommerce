using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        private readonly IProductReadRepository _productReadRepository;
        public ProductController(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;

        }
        public async Task<IActionResult> GetAll()
        {
            return Ok(_productReadRepository.GetAll().ToList());
        }
    }
}
