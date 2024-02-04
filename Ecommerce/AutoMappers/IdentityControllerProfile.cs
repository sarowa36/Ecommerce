using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.IdentityController;

namespace Ecommerce.AutoMappers
{
    public class IdentityControllerProfile : Profile
    {
        public IdentityControllerProfile()
        {
            CreateMap<ApplicationUser, GetUserVM>().ForMember(x => x.Roles, opt => opt.Ignore()).ReverseMap();
        }
    }
}
