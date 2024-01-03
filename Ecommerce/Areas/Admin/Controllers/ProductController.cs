using AutoMapper;
using BusinessLayer.Validations.Admin.ProductController;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Admin.ProductController;
using Microsoft.AspNetCore.Mvc;
using ToolsLayer.ErrorModel;
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
        public async Task<IActionResult> GetList(int page, int itemsPerPage, string? sortKey, string? sortOrder)
        {
            // camelCase to PascalCase
            if (sortKey != null)
                sortKey = sortKey.First().ToString().ToUpper() + sortKey.Substring(1);
            return Ok(new
            {
                values = _productReadRepository.GetAll(
                    (page - 1) * itemsPerPage, 
                    itemsPerPage,
                    sortKey,
                    !string.IsNullOrWhiteSpace(sortOrder) && sortOrder == "desc")
                .ToList()
                .Select(x => _mapper.Map<ListProductValueVM>(x)
                ),
                totalCount = _productReadRepository.Count()
            });
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
                    modelAsProduct.Images.Add(img.File != null ? await img.File.SaveFileAsync(Path.Combine("Admin", "Product")) : img.Link);
                }
                await _productWriteRepository.UpdateAsync(modelAsProduct);
                await _productWriteRepository.SaveChangesAsync();
                return Ok();
            }
            if (!exist)
                return BadRequest(new { modelOnly = "This value doesnt exist" });
            return BadRequest(res.ToErrorModel());
        }

    }
}
