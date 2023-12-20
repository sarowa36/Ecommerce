using AutoMapper;
using BusinessLayer.Validations.Admin.ProductController;
using DataAccessLayer;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Repositories;
using Ecommerce.Helpers;
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
        private readonly IProductReadRepository _productReadRepository;
        public ProductController(IProductWriteRepository productWriteRepository, IMapper mapper, IServiceProvider serviceProvider, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _productReadRepository = productReadRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM model)
        {
            var files = HttpContext.Request.Form.Files;
            var res = _serviceProvider.GetService<CreateProductVMValidation>().Validate(model);
            if (res.IsValid)
            {
                var modelAsProduct = _mapper.Map<Product>(model);
                modelAsProduct.Images = new List<string>();
                foreach (var img in model.Images)
                {
                    modelAsProduct.Images.Add(img.File != null ? await img.File.SaveFileAsync(Path.Combine("Admin", "Product")) : img.Link);
                }
                await _productWriteRepository.CreateAsync(modelAsProduct);
                await _productWriteRepository.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(res.ToErrorModel());
        }
        public async Task<IActionResult> GetList()
        {
            return Ok(_productReadRepository.GetAll().ToList().Select(x => _mapper.Map<ListProductValueVM>(x)));
        }
        public async Task<IActionResult> Update(int id)
        {
            return Ok(_mapper.Map<UpdateGetProductVM>(_productReadRepository.Get(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostProductVM model)
        {
            var files = HttpContext.Request.Form.Files;
            var res = _serviceProvider.GetService<UpdatePostProductVMValidation>().Validate(model);
            var exist = _productReadRepository.Exist(x => x.Id == model.Id);
            if (res.IsValid && exist)
            {
                var modelAsProduct = _mapper.Map<Product>(model);
                modelAsProduct.Images = new List<string>();
                foreach (var img in model.Images)
                {
                    modelAsProduct.Images.Add(img.File!=null ? await img.File.SaveFileAsync(Path.Combine("Admin", "Product")) : img.Link);
                }
                await _productWriteRepository.UpdateAsync(modelAsProduct);
                await _productWriteRepository.SaveChangesAsync();
                return Ok();
            }
            if(!exist)
                return BadRequest(new{modelOnly="This value doesnt exist" });
            return BadRequest(res.ToErrorModel());
        }
        
    }
}
