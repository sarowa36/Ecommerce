using AutoMapper;
using EntityLayer.DTOs.Areas.User.AddressController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.AddressController;

namespace Ecommerce.AutoMappers.User
{
    public class AddressControllerProfile : Profile
    {
        public AddressControllerProfile()
        {
            CreateMap<CreateAddressDTO, UserAddress>().ReverseMap();
            CreateMap<UpdateAddressDTO, UserAddress>().ReverseMap();
            CreateMap<UserAddress, AddressListValueVM>();
        }
    }
}
