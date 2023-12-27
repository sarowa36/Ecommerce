using DataAccessLayer;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Middlewares
{
    public static class AuthorizationExtension
    {
        public static void AddAppAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization()
                .AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<ADC>()
                .AddDefaultTokenProviders();
        }
    }
}
