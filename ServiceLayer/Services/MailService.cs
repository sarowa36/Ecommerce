using EntityLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ServiceLayer.Base.Services;
using ServiceLayer.Helpers;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ServiceLayer.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;
        readonly IHttpContextAccessor _httpContextAccessor;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        public MailService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
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

            mail.From = new(_configuration["Mail:Username"], "Salsha", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            //smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }
        public async Task SendConfirmationMail(ApplicationUser user,string token)
        {
            await SendMailAsync(user.Email, "Email adresinizi doğrulayınız", await GetEmailView.GetConfirmationEmailViewAsync(HttpContext,user.Email, token));
        }
        public async Task SendApprovedOrderMail(string to,int orderId)
        {
            await SendMailAsync(to, "Siparişiniz onaylandı", await GetEmailView.GetOrderEmailViewAsync(HttpContext, orderId));
        }
        public async Task SendPasswordResetMailAsync(ApplicationUser user, string token)
        {
            await SendMailAsync(user.Email, "Şifre yenileme isteği", await GetEmailView.GetResetPasswordEmailView(HttpContext, user.Email, token));
        }
    }
}
