using DataAccessLayer;
using EntityLayer.Entities;
using IdentityLayer.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityLayer
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddMyIdentity(this IServiceCollection services)
        {
            services.AddAppAuthentication();
            services.AddAppAuthorization();
            return services;
        }
        public static void AddAppAuthentication(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options => {
                options.SignIn.RequireConfirmedEmail = true;
            });
            services.AddAuthentication()
                .AddApplicationCookie()
                .PostConfigure(opt =>
                {
                    opt.LoginPath = "/Login";
                    opt.Events.OnRedirectToLogin = OnRedirectToLogin;

                });
        }

        public static void AddAppAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization()
                .AddScoped<IMyUserManager<ApplicationUser>, MyUserManager<ApplicationUser>>()
                .AddScoped<IMyRoleManager<IdentityRole>, MyRoleManager<IdentityRole>>()
                .AddScoped<IMySignInManager<ApplicationUser>, MySignInManager<ApplicationUser>>()
                .AddIdentityCore<ApplicationUser>(option =>
                {
                    //option.SignIn.RequireConfirmedEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddUserManager<MyUserManager<ApplicationUser>>()
                .AddSignInManager<MySignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<ADC>()
                .AddDefaultTokenProviders();
        }
        private static Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
           
            if (context.Request.Headers.Referer.Count > 0)
            {
                var refererPathUri = new Uri(context.Request.Headers.Referer);
                var refererPath = refererPathUri.PathAndQuery;
                var redirectUriBuilder = new UriBuilder(context.RedirectUri);
                if (refererPathUri.AbsolutePath != redirectUriBuilder.Uri.AbsolutePath)
                {
                    redirectUriBuilder.Query = QueryString.Create(context.Options.ReturnUrlParameter, refererPath).Value;
                    context.RedirectUri = redirectUriBuilder.Uri.AbsoluteUri;
                }
            }
            context.Response.StatusCode = StatusCodes.Status302Found;
            context.Response.WriteAsJsonAsync(new { redirect = context.RedirectUri });
            return Task.CompletedTask;
        }
    }
}
