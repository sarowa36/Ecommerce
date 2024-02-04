using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.CommonVM;
using EntityLayer.ViewModels.User.ShoppingCartController;

namespace Ecommerce.AutoMappers.User
{
    public class ShoppingCartControllerProfile : Profile
    {
        public ShoppingCartControllerProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemValueVM>().ForMember(x => x.ProductImage, opt => opt.MapFrom(x => x.Product.Images.FirstOrDefault()))
                .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.Product.Price));
        }
    }
}
