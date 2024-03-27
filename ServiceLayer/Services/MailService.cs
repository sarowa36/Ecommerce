using EntityLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using ServiceLayer.Base.Services;
using ServiceLayer.Helpers;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using ToolsLayer.FileManagement;

namespace ServiceLayer.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IWebHostEnvironment _iwhe;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        public MailService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment iwhe)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _iwhe = iwhe;
        }
        public async Task SendMailAsync(string to, string subject, string body, List<LinkedResource> images=default)
        {
            await SendMailAsync(new[] { to }, subject, body,images);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body,List<LinkedResource> images=default)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = true;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.From = new(_configuration["Mail:Username"], "Salsha", System.Text.Encoding.UTF8);

            AlternateView altView = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            // Logo 1 //
            var PictureRes = new LinkedResource(Path.Combine(_iwhe.WebRootPath, "logo_white.png"), "image/png");
            PictureRes.ContentId = "logo_white";
            PictureRes.ContentType.Name = "logo_white.png";
            altView.LinkedResources.Add(PictureRes);
            if(images!=null && images.Count>0) 
            images.ForEach(x => altView.LinkedResources.Add(x));
            mail.AlternateViews.Add(altView);

            SmtpClient smtp = new();
            //smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
            PictureRes.Dispose();
        }
        public async Task SendConfirmationMail(ApplicationUser user,string token)
        {
            await SendMailAsync(user.Email, "Email adresinizi doğrulayınız", await GetEmailView.GetConfirmationEmailViewAsync(HttpContext,user.Email, token));
        }
        public async Task SendApprovedOrderMail(string to,Order order)
        {
            var a = new List<LinkedResource>();
            
            await SendMailAsync(to, "Siparişiniz onaylandı", 
                await GetEmailView.GetOrderEmailViewAsync(HttpContext, order.Id), 
                order.OrderItems.GroupBy(x => x.ProductId).Select(x =>
                {
                    var item = x.FirstOrDefault();
                    var res = new LinkedResource(Path.Combine(_iwhe.WebRootPath, item.ProductImage),MimeTypes.GetMimeType( Path.GetExtension(item.ProductImage)));
                    res.ContentId = "product_" + item.ProductId;
                    res.ContentType.Name = item.ProductName+ Path.GetExtension(item.ProductImage);
                    return res;
                }).ToList());
        }
        public async Task SendPasswordResetMailAsync(ApplicationUser user, string token)
        {
            await SendMailAsync(user.Email, "Şifre yenileme isteği", await GetEmailView.GetResetPasswordEmailView(HttpContext, user.Email, token));
        }
    }
}
