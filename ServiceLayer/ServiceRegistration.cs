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
            services.AddScoped<IIyziPayService , IyziPayService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IServiceErrorContainer, ServiceErrorContainer>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<IUserAddressService, UserAddressService>();
            services.AddScoped<IOrderRefundService, OrderRefundService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<CustomerSaveService>();
        }
    }
}
