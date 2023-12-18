using AutoMapper;
using BusinessLayer.Validations.Admin.ProductController;
using DataAccessLayer;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Repositories;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Admin.ProductController;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToolsLayer.FileManagement;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;
        private readonly IServiceProvider _serviceProvider;
        public ProductController(IProductWriteRepository productWriteRepository, IMapper mapper,IServiceProvider serviceProvider)
        {
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM model)
        {
            var files=HttpContext.Request.Form.Files;
            var res = _serviceProvider.GetService<CreateProductVMValidation>().Validate(model);
            if (res.IsValid)
            {
                var modelAsProduct = _mapper.Map<Product>(model);
                modelAsProduct.Images = new List<string>();
                foreach (var img in model.Images)
                {
                   modelAsProduct.Images.Add(await img.SaveFileAsync(Path.Combine("Admin","Product"))); 
                }
                await _productWriteRepository.CreateAsync(modelAsProduct);
                await _productWriteRepository.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(res.Errors);
        }
    }
}
