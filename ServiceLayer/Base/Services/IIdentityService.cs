using EntityLayer.Entities;
using ServiceLayer.ServiceCommand.MailService;

namespace ServiceLayer.Base.Services
{
    public interface IIdentityService
    {
        Task<PasswordResetCommand> CreatePasswordResetRequestAsync(ApplicationUser user);
        Task CreateUserAsync(ApplicationUser user, string password);
        Task<ApplicationUser> GetCurrentUserAsync();
        Task<ApplicationUser> GetUser(string usernameOrEmail);
        Task LoginAsync(ApplicationUser user);
        Task LoginAsync(ApplicationUser user, string password);
        Task LoginAsync(string usernameOrEmail, string password);
        Task UpdatePasswordAsync(ApplicationUser user, string resetToken, string newPassword);
        Task VerifyResetTokenAsync(string resetToken, ApplicationUser user);
    }
}