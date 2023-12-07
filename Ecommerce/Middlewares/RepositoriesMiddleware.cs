using DataAccessLayer.Base;
using DataAccessLayer.RepositoryEvents;
using EntityLayer.Base;
using System.Reflection;

namespace Ecommerce.Middlewares
{
    public static class RepositoriesMiddleware
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            GetRepositoryTypes().ForEach(repo => services.AddScoped(repo));
            return services;
        }
        public static List<Type> GetRepositoryTypes()
        {
            return Assembly.Load("EntityLayer").GetTypes().Where(x => x.Namespace == "EntityLayer.Concrete")
                .Select(entity => Assembly.Load("DataAccessLayer").GetTypes()
                .FirstOrDefault(x => typeof(GenericBaseRepository<>).MakeGenericType(entity).IsAssignableFrom(x)
                )
                ).Where(x => x != null).ToList();
        }
    }
}
