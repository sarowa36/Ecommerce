using AutoMapper;
using EntityLayer.DTOs.Areas.User.CustomerSaveController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.CustomerSaveController;

namespace Ecommerce.AutoMappers.User
{
    public class CustomerSaveControllerProfile:Profile
    {
        public CustomerSaveControllerProfile()
        {
            CreateMap<CreateCustomerSaveDTO, CustomerSave>().ForMember(x=>x.Items,opt=>opt.PreCondition(x=>x.ProductId!=null)).ForMember(x => x.Items, opt => opt.MapFrom(x => new List<CustomerSaveItem>() { new CustomerSaveItem() { ProductId = (int)x.ProductId } }));
            CreateMap<UpdateCustomerSaveDTO, CustomerSave>();
            CreateMap<CustomerSave, CustomerSaveVM>();
            CreateMap<CustomerSaveItem, CustomerSaveItemVM>();
            CreateMap<Product, CustomerSaveItemProductVM>();
        }
    }
}
