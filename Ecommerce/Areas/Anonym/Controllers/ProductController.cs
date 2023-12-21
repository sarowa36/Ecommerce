using AutoMapper;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.ViewModels.Anonym.ProductController;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Areas.Anonym.Controllers
{
    [Area("Anonym")]
    public class ProductController : Controller
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductReadRepository productReadRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetList()
        {
            return Ok(_productReadRepository.GetAll().ToList().Select(x => _mapper.Map<ListProductValueVM>(x)));
        }
        public async Task<IActionResult> Get(int id)
        {
            if (_productReadRepository.Get(id, out var entity))
                return Ok(_mapper.Map<GetProductValueVM>(entity));
            return NotFound();
        }
    }
}
