using AutoMapper;
using EntityLayer.ViewModels.Anonym.CategoryController;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.Anonym.Controllers
{
    [Area("Anonym")]
    public class CategoryController : Controller
    {
        readonly ICategoryService _categoryService;
        readonly IServiceErrorContainer _errorContainer;
        readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IServiceErrorContainer errorContainer, IMapper mapper)
        {
            _categoryService = categoryService;
            _errorContainer = errorContainer;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetList()
        {
            var categories =_errorContainer.AddServiceResponse(()=>_categoryService.GetAll());
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<List<CategoryListValueVM>>(categories)) :BadRequest(_errorContainer.Errors);
        }
    }
}
