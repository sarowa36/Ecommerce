using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Anonym.ProductController;

namespace Ecommerce.AutoMappers.Anonym
{
    public class ProductControllerProfile : Profile
    {
        public ProductControllerProfile()
        {
            CreateMap<Product, ListProductValueVM>().ForMember(x => x.Image, opt => opt.MapFrom(y => y.Images.FirstOrDefault())).ReverseMap();
            CreateMap<Product, GetProductValueVM>().ReverseMap();
        }
    }
}
