using Hub.Storage.Core.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Storage.Repository
{
    public static class StorageServiceCollectionExtensions
    {
        public static void AddDbContext<TDbContext>(this IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey, string migrationAssembly)
            where TDbContext : HubDbContext
        {
            serviceCollection.AddDbContext<TDbContext>(options => 
                options.UseSqlServer(configuration.GetValue<string>(connectionStringKey), 
                    x => x.MigrationsAssembly(migrationAssembly)));
            
            serviceCollection.TryAddSingleton<IHubDbRepository, HubDbRepository<TDbContext>>();
        }
        
        public static void AddDbContext<TDbContext>(this IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey)
            where TDbContext : HubDbContext
        {
            serviceCollection.AddDbContext<TDbContext>(options => 
                options.UseSqlServer(configuration.GetValue<string>(connectionStringKey)));
            
            serviceCollection.TryAddSingleton<IHubDbRepository, HubDbRepository<TDbContext>>();
        }
    }
}