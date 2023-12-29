using EntityLayer.Entities;
using EntityLayer.ViewModels.IdentityController;
using ServiceLayer.ServiceResults.IdentityService;

namespace ServiceLayer.Base.Services
{
    public interface IIdentityService
    {
        Task<AssignRolesToUserResponse> AssignRolesToUserAsnyc(string userId, string[] roles);
        Task<CreateUserResponse> CreateUserAsync(RegisterVM model);
        Task<CreatePasswordResetResponse> CreatePasswordResetRequestAsync(string email);
        Task<GetUserRolesResponse> GetUserRolesAsync(string userIdOrName);
        Task<GetUserRolesResponse> GetUserRolesAsync(ApplicationUser user);
        Task<GetUserRolesResponse> GetUserRolesAsync();
        Task<LoginResponse> LoginAsync(string usernameOrEmail, string password);
        Task<LoginResponse> LoginAsync(ApplicationUser user, string password);
        Task<LoginResponse> LoginAsync(ApplicationUser user);
        Task<UpdatePasswordResponse> UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
        Task<GetCurrentUserResponse> GetCurrentUserAsync();
    }
}