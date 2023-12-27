using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Middlewares
{
    public static class AuthenticationExtension
    {
        public static void AddAppAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddApplicationCookie()
                .PostConfigure(opt =>
                {
                    opt.LoginPath = "/Login";
                    opt.Events.OnRedirectToLogin = OnRedirectToLogin;
                });
        }
        private static Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            var refererPathUri = new Uri(context.Request.Headers.Referer);
            var refererPath = refererPathUri.PathAndQuery;
            var redirectUriBuilder = new UriBuilder(context.RedirectUri);
            if (refererPathUri.AbsolutePath != redirectUriBuilder.Uri.AbsolutePath)
            {
                redirectUriBuilder.Query = QueryString.Create(context.Options.ReturnUrlParameter, refererPath).Value;
                context.RedirectUri = redirectUriBuilder.Uri.AbsoluteUri;
                context.Response.StatusCode = StatusCodes.Status302Found;
                context.Response.WriteAsJsonAsync(new { redirect = context.RedirectUri });
            }
            return Task.CompletedTask;
        }
    }
}
