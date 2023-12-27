using EntityLayer.ViewModels.IdentityController;
using ServiceLayer.ServiceResults.Identity;

namespace ServiceLayer.Base.Services
{
    public interface IIdentityService
    {
        Task<AssignRolesToUserResponse> AssignRolesToUserAsnyc(string userId, string[] roles);
        Task<CreateUserResponse> CreateUserAsync(RegisterVM model);
        Task<CreatePasswordResetResponse> CreatePasswordResetRequestAsync(string email);
        Task<GetUserRolesResponse> GetUserRolesAsync(string userIdOrName);
        Task<LoginResponse> LoginAsync(string usernameOrEmail, string password);
        Task<UpdatePasswordResponse> UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}