using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.OrderController;
using EntityLayer.ViewModels.User.OrderRefundController;

namespace Ecommerce.AutoMappers.User
{
    public class OrderControllerProfile : Profile
    {
        public OrderControllerProfile()
        {
            CreateMap<OrderItem, UserOrderItemVM>();
            CreateMap<Order, UserOrderVM>();
            CreateMap<OrderItem, UserOrderRefundItemVM>();
            CreateMap<OrderRefund, UserOrderRefundVM>().ForMember(x => x.RefundItems, opt => opt.MapFrom(y => y.OrderRefundOrderItems.Select(x => x.OrderItem)));
        }
    }
}
