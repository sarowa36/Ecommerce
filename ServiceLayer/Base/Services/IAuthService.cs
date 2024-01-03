using EntityLayer.Entities;

namespace ServiceLayer.Base.Services
{
    public interface IAuthService
    {
        Task AssignRolesToUserAsnyc(ApplicationUser user, string[] roles);
        Task AssignUserRole(ApplicationUser user);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
    }
}