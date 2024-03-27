using EntityLayer.Entities;
using System.Net.Mail;

namespace ServiceLayer.Base.Services
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, List<LinkedResource> images);
        Task SendMailAsync(string[] tos, string subject, string body, List<LinkedResource> images);
        Task SendConfirmationMail(ApplicationUser user, string token);
        Task SendApprovedOrderMail(string to, Order order);
        Task SendPasswordResetMailAsync(ApplicationUser user, string token);
    }
}