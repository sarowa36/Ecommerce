using AutoMapper;
using BusinessLayer.Validations.Admin.CategoryController;
using EntityLayer.DTOs.Areas.Admin.CategoryController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Admin.CategoryController;
using EntityLayer.ViewModels.Anonym.CategoryController;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _categoryService;
        readonly IServiceErrorContainer _errorContainer;
        readonly IServiceProvider _serviceProvider;
        readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IServiceProvider serviceProvider, IServiceErrorContainer errorContainer, IMapper mapper)
        {
            _categoryService = categoryService;
            _serviceProvider = serviceProvider;
            _errorContainer = errorContainer;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetList()
        {
            return Ok(_mapper.Map<List<CategoryListValueVM>>(await _categoryService.GetAll()));
        }
        public async Task<IActionResult> GetListWithParentNames(int? id)
        {
            return Ok(_mapper.Map<List<CategoryListValueVM>>(await _categoryService.GetAllWithParentName(id)));
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<CreateCategoryDTOValidation>().Validate(model));
            _errorContainer.AddServiceResponse(() => _categoryService.Create(_mapper.Map<Category>(model)));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
           var returnVal= _errorContainer.AddServiceResponse(() => _categoryService.Get(id));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<CategoryUpdateVM>(returnVal)) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<UpdateCategoryDTOValidation>().Validate(model));
            var category= _errorContainer.AddServiceResponse(() => _categoryService.Get(model.Id));
            _errorContainer.AddServiceResponse(() => _categoryService.Update(_mapper.Map(model,category)));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        public async Task<IActionResult> UpdateSortIndex(Dictionary<int, int> sortIndex)
        {
            _errorContainer.AddServiceResponse(() => _categoryService.UpdateSortIndex(sortIndex));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        public async Task<IActionResult> Delete(int id)
        {
            _errorContainer.AddServiceResponse(() => _categoryService.Delete(id));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
    }
}
