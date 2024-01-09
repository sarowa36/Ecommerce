using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using ServiceLayer.Services;

namespace ServiceLayer
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IPaymentService , PaymentService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IServiceErrorContainer, ServiceErrorContainer>();
            services.AddScoped<OrderService>();
            services.AddScoped<IUserAddressService, UserAddressService>();
        }
    }
}
