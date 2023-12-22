
using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.ShoppingCartController;

namespace Ecommerce.AutoMappers.User.ShoppingCartController
{
    public class ShoppingCartItemProfile:Profile
    {
        public ShoppingCartItemProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemValueVM>().ForMember(x => x.ProductImage, opt => opt.MapFrom(x => x.Product.Images.FirstOrDefault()))
                .ForMember(x => x.ProductPrice, opt => opt.MapFrom(x => x.Product.Price));
        }
    }
}
