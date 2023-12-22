using AutoMapper;
using EntityLayer.Entities;
using EntityLayer.ViewModels.IdentityController;

namespace Ecommerce.AutoMappers.IdentityController
{
    public class ApplicationUserProfile:Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser,GetUserVM>().ForMember(x=>x.Roles,opt=>opt.Ignore()).ReverseMap();
        }
    }
}
