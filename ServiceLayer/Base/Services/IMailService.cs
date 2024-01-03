using ServiceLayer.ServiceCommand.MailService;

namespace ServiceLayer.Base.Services
{
    public interface IMailService
    {
        Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName);
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendPasswordResetMailAsync(PasswordResetCommand passwordResetCommand);
    }
}