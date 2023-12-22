using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using DataAccessLayer.Repositories.ShoppingCartItemRepositories;
using EntityLayer.Base;
using System.Reflection;

namespace Ecommerce.Middlewares
{
    public static class RepositoriesMiddleware
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IProductWriteRepository,ProductWriteRepository>()
                .AddScoped<IProductReadRepository,ProductReadRepository>()
                .AddScoped<IShoppingCartItemReadRepository,ShoppingCartItemReadRepository>()
                .AddScoped<IShoppingCartItemWriteRepository, ShoppingCartItemWriteRepository>();
        }
    }
}
