using DataAccessLayer.Base.Repositories.OrderRepositories;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using DataAccessLayer.Base.Repositories.UserAddressRepositories;
using DataAccessLayer.Repositories.OrderRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using DataAccessLayer.Repositories.ShoppingCartItemRepositories;
using DataAccessLayer.Repositories.UserOrderRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessLayerServices(this IServiceCollection services)
        {
            services
                .AddScoped<IProductWriteRepository, ProductWriteRepository>()
                .AddScoped<IProductReadRepository, ProductReadRepository>()
                .AddScoped<IShoppingCartItemReadRepository, ShoppingCartItemReadRepository>()
                .AddScoped<IShoppingCartItemWriteRepository, ShoppingCartItemWriteRepository>()
                .AddScoped<IOrderReadRepository,OrderReadRepository>()
                .AddScoped<IOrderWriteRepository, OrderWriteRepository>()
                .AddScoped<IUserAddressReadRepository,UserAddressReadRepository>()
                .AddScoped<IUserAddressWriteRepository,UserAddressWriteRepository>();
        }
    }
}
