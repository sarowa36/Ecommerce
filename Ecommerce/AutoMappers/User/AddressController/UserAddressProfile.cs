using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.AddressController;

namespace Ecommerce.AutoMappers.User.AddressController
{
    public class UserAddressProfile:Profile
    {
        public UserAddressProfile()
        {
            CreateMap<CreateAddressVM, UserAddress>().ReverseMap();
            CreateMap<UpdateAddressVM, UserAddress>().ReverseMap();
            CreateMap<UserAddress, AddressListValueVM>();
        }
    }
}
