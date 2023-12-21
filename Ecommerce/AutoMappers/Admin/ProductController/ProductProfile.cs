using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Admin.ProductController;

namespace Ecommerce.AutoMappers.Admin.ProductController
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, CreateProductVM>().ForMember(x=>x.Images,opt=>opt.Ignore());
            CreateMap<CreateProductVM,Product>().ForMember(x => x.Images, opt => opt.Ignore());
            CreateMap<Product, ListProductValueVM>().ForMember(x=>x.Image,opt=>opt.MapFrom(y=>y.Images.FirstOrDefault())).ReverseMap();
            CreateMap<Product, UpdateGetProductVM>();
            CreateMap<UpdatePostProductVM,Product>();
        }
    }
}
