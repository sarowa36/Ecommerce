using DataAccessLayer.Base.JsonData;
using DataAccessLayer.Base.Repositories.CategoryRepositories;
using DataAccessLayer.Base.Repositories.OrderItemRepositories;
using DataAccessLayer.Base.Repositories.OrderRefundRepositories;
using DataAccessLayer.Base.Repositories.OrderRepositories;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using DataAccessLayer.Base.Repositories.UserAddressRepositories;
using DataAccessLayer.JsonData;
using DataAccessLayer.Repositories.CategoryRepositories;
using DataAccessLayer.Repositories.OrderItemRepositories;
using DataAccessLayer.Repositories.OrderRefundRepositories;
using DataAccessLayer.Repositories.OrderRepositories;
using DataAccessLayer.Repositories.ProductRepositories;
using DataAccessLayer.Repositories.ShoppingCartItemRepositories;
using DataAccessLayer.Repositories.UserAddressRepositories;
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
                .AddScoped<IOrderReadRepository, OrderReadRepository>()
                .AddScoped<IOrderWriteRepository, OrderWriteRepository>()
                .AddScoped<IOrderItemReadRepository, OrderItemReadRepository>()
                .AddScoped<IOrderItemWriteRepository, OrderItemWriteRepository>()
                .AddScoped<IOrderRefundReadRepository, OrderRefundReadRepository>()
                .AddScoped<IOrderRefundWriteRepository, OrderRefundWriteRepository>()
                .AddScoped<IUserAddressReadRepository, UserAddressReadRepository>()
                .AddScoped<IUserAddressWriteRepository, UserAddressWriteRepository>()
                .AddScoped<ICitiesAndDistrictsValues,CitiesAndDistrictsValues>()
                .AddScoped<ICategoryReadRepository,CategoryReadRepository>()
                .AddScoped<ICategoryWriteRepository,CategoryWriteRepository>();
        }
    }
}
