using AutoMapper;
using BusinessLayer.Validations.Admin.Product;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.ViewModels.Admin.ProductController;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToolsLayer.FileManagement;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        public ProductController(ProductRepository productRepository, IMapper mapper,IServiceProvider serviceProvider)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM model)
        {
            var res = _serviceProvider.GetService<CreateProductVMValidation>().Validate(model);
            if (res.IsValid)
            {
                var modelAsProduct = _mapper.Map<Product>(model);
                modelAsProduct.Images = new List<string>();
                foreach (var img in model.Images)
                {
                   modelAsProduct.Images.Add(await img.SaveFileAsync(Path.Combine("Admin","Product")));
                }
                _productRepository.Create(modelAsProduct);
                return Ok();
            }
            return BadRequest(res.Errors);
        }
    }
}
