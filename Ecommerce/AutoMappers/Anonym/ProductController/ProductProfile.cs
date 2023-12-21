using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Anonym.ProductController;

namespace Ecommerce.AutoMappers.Anonym.ProductController
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ListProductValueVM>().ForMember(x=>x.Image,opt=>opt.MapFrom(y=>y.Images.FirstOrDefault())).ReverseMap();
            CreateMap<Product, GetProductValueVM>().ReverseMap();
        }
    }
}
