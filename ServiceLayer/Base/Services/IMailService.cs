using EntityLayer.Entities;

namespace ServiceLayer.Base.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendConfirmationMail(ApplicationUser user, string token);
        Task SendApprovedOrderMail(string to, int orderId);
        Task SendPasswordResetMailAsync(ApplicationUser user, string token);
    }
}