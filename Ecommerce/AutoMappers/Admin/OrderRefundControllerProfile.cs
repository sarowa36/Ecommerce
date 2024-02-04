using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.Admin.OrderRefundController;

namespace Ecommerce.AutoMappers.Admin
{
    public class OrderRefundControllerProfile : Profile
    {
        public OrderRefundControllerProfile()
        {
            CreateMap<OrderItem, AdminOrderRefundItemVM>();
            CreateMap<OrderRefund, AdminOrderRefundVM>().ForMember(x => x.RefundItems, opt => opt.MapFrom(y => y.OrderRefundOrderItems.Select(x => x.OrderItem)));
        }
    }
}
