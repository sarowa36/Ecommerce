using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.OrderController;

namespace Ecommerce.AutoMappers.User.OrderController
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem,UserOrderItemVM>();
            CreateMap<Order, UserOrderVM>();
        }
    }
}
