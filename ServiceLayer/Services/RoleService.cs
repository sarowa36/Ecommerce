using EntityLayer.Entities;
using IdentityLayer.Base;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using ToolsLayer.ErrorModel;

namespace ServiceLayer.Services
{
    public class RoleService : IRoleService
    {

        readonly IServiceErrorContainer _serviceErrorContainer;
        readonly IMyUserManager<ApplicationUser> _userManager;

        public RoleService(IMyUserManager<ApplicationUser> userManager,
            IServiceErrorContainer serviceErrorProvider)
        {
            _userManager = userManager;
            _serviceErrorContainer = serviceErrorProvider;
        }

        public async Task AssignUserRole(ApplicationUser user)
        {
            _serviceErrorContainer.BindError((await _userManager.AddToRoleAsync(user, "User")).ToErrorModel());
        }
        public async Task AssignRolesToUserAsnyc(ApplicationUser user, string[] roles)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRolesAsync(user, roles);
        }
        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }
    }
}
