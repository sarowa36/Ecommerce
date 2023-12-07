using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public async Task<IActionResult> GetAll()
        {
            return Ok(_productRepository.GetAll().ToList());
        }
        public async Task<IActionResult> AddRange()
        {
           /* _productRepository.CreateRange(new List<Product>() {
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            new(){ ProductName=Guid.NewGuid().ToString(),ProductDescription=Guid.NewGuid().ToString()},
            });*/
            return Ok();
        }
    }
}
