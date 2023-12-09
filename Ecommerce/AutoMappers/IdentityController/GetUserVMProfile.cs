using AutoMapper;
using EntityLayer.Concrete;
using EntityLayer.ViewModels.IdentityController;

namespace Ecommerce.AutoMappers.IdentityController
{
    public class GetUserVMProfile:Profile
    {
        public GetUserVMProfile()
        {
            CreateMap<ApplicationUser,GetUserVM>().ForMember(x=>x.Roles,opt=>opt.Ignore()).ReverseMap();
        }
    }
}
