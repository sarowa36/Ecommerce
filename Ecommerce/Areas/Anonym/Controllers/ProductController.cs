using AutoMapper;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.Enum;
using EntityLayer.ViewModels.Anonym.ProductController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> GetList([FromQuery(Name = "categories[]")]List<int> categories, ProductSortByEnum sort, int? min,int? max,string? search,int page=1)
        {
            var list = _productReadRepository.GetAll();
            if (categories != null && categories.Count > 0)
                list = list.Where(x => categories.Contains((int)x.CategoryId));
            if (min != null && min > 0)
                list = list.Where(x => min < x.Price);
            if (max != null && max > 0)
                list = list.Where(x => max > x.Price);
            if (!string.IsNullOrEmpty(search)) {
                search = "%" + search + "%";
                list=list.Where(x=>EF.Functions.Like(x.Name, search) || EF.Functions.Like(x.Description, search) || EF.Functions.Like(x.Category.Name, search));
            }
            if (sort == ProductSortByEnum.NewToOld || sort == ProductSortByEnum.HigherRate)
                list = list.OrderByDescending(x => x.CreateDate);
            else if (sort == ProductSortByEnum.HigherPrice)
                list = list.OrderByDescending(x => x.Price);
            else if (sort == ProductSortByEnum.LowerPrice)
                list = list.OrderBy(x => x.Price);
            var indexedList = list.Skip((page-1)*24).Take(24);
           return Ok(new{ values= indexedList.ToList().Select(x => _mapper.Map<ListProductValueVM>(x)),count= list.Count() });
        }
        public async Task<IActionResult> Get(int id)
        {
            if (_productReadRepository.Get(id, out var entity))
                return Ok(_mapper.Map<GetProductValueVM>(entity));
            return NotFound();
        }
    }
}
