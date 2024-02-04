using AutoMapper;
using EntityLayer.DTOs.Areas.Admin.ProductController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Admin.ProductController;

namespace Ecommerce.AutoMappers.Admin
{
    public class ProductControllerProfile : Profile
    {
        public ProductControllerProfile()
        {
            CreateMap<Product, CreateProductDTO>().ForMember(x => x.Images, opt => opt.Ignore());
            CreateMap<CreateProductDTO, Product>().ForMember(x => x.Images, opt => opt.Ignore());
            CreateMap<Product, ListProductValueVM>().ForMember(x => x.Image, opt => opt.MapFrom(y => y.Images.FirstOrDefault())).ReverseMap();
            CreateMap<Product, UpdateProductVM>();
            CreateMap<UpdateProductDTO, Product>();
        }
    }
}
