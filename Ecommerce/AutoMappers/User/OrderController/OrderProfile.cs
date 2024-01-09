using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.OrderController;

namespace Ecommerce.AutoMappers.User.OrderController
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem,UserOrderItemVM>().ForMember(x=>x.ProductId,opt=>opt.MapFrom(y=>y.ProductId))
                .ForMember(x => x.ProductImage, opt => opt.MapFrom(y => y.Product.Images.FirstOrDefault()))
                .ForMember(x=>x.ProductName,opt=>opt.MapFrom(y=>y.Product.Name))
                .ForMember(x=>x.Price,opt=>opt.MapFrom(y=>y.Product.Price));
            CreateMap<Order, UserOrderVM>();
        }
    }
}
