using EntityLayer.DTOs.Controllers.IdentityController;
using EntityLayer.Entities;

namespace ServiceLayer.Base.Services
{
    public interface IIdentityService
    {
        Task<string> CreatePasswordResetTokenAsync(ApplicationUser user);
        Task PasswordResetAsync(ApplicationUser user, string resetToken, string newPassword);
        Task PasswordResetAsync(ApplicationUser user, ChangePasswordDTO model);
        Task CreateUserAsync(ApplicationUser user, string password);
        Task<string> EmailConfirmationTokenCreate(ApplicationUser user);
        
        Task EmailConfirm(ApplicationUser user, string token);
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<ApplicationUser> GetUser(string usernameOrEmail);
        Task Logout();
        Task LoginAsync(ApplicationUser user);
        Task LoginAsync(ApplicationUser user, string password);
        Task LoginAsync(string usernameOrEmail, string password);
    }
}