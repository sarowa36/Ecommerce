using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Base.Services;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
        }
    }
}
