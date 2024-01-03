using Microsoft.Extensions.Configuration;
using ServiceLayer.Base.Services;
using ServiceLayer.ServiceCommand.MailService;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ServiceLayer.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "Ecommerce By Sarowa36", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(PasswordResetCommand passwordResetCommand)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello<br>If you have requested a new password, you can renew your password from the link below.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine("");
            mail.AppendLine("/updatePassword/");
            mail.AppendLine(passwordResetCommand.UserId);
            mail.AppendLine("/");
            mail.AppendLine(passwordResetCommand.Token);
            mail.AppendLine("\">Click here for new password request...</a></strong><br><br><span style=\"font-size:12px;\">NOTE : If this request has not been made by you, please do not take this e-mail seriously.</span><br><br><br>Ecommerce By Sarowa36");

            await SendMailAsync(passwordResetCommand.Email, "Password Renewal Request", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $"Dear {userName} Hello<br>" +
                $"Your order with the code {orderCode} that you placed on {orderDate} has been completed and given to the shipping company.";

            await SendMailAsync(to, $"Your Order with Order Number {orderCode} is Complete", mail);

        }
    }
}
