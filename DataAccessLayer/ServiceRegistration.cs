using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using DataAccessLayer.Repositories.ShoppingCartItemRepositories;
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
                .AddScoped<IShoppingCartItemWriteRepository, ShoppingCartItemWriteRepository>();
        }
    }
}
