using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Hub.Storage.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Storage.DependencyRegistration
{
    public static class StorageServiceCollectionExtensions
    {
        public static void TryAddBackgroundTaskConfigurationSingletons(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddSingleton<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
        }
        
        public static void TryAddBackgroundTaskConfigurationTransients(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddTransient<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
        }
        
        public static void TryAddBackgroundTaskConfigurationScoped(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddScoped<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddScoped<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
        }
        
        public static void TryAddSettingSingletons(this IServiceCollection serviceCollection) 
        {
            serviceCollection.TryAddSingleton<ISettingFactory, SettingFactory>();
            serviceCollection.TryAddSingleton<ISettingProvider, SettingProvider>();
        }
        
        public static void TryAddSettingTransients(this IServiceCollection serviceCollection) 
        {
            serviceCollection.TryAddTransient<ISettingFactory, SettingFactory>();
            serviceCollection.TryAddTransient<ISettingProvider, SettingProvider>();
        }
        
        public static void TryAddSettingScoped(this IServiceCollection serviceCollection) 
        {
            serviceCollection.TryAddScoped<ISettingFactory, SettingFactory>();
            serviceCollection.TryAddScoped<ISettingProvider, SettingProvider>();
        }

        public static void TryAddWorkerLogSingletons(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IWorkerLogFactory, WorkerLogFactory>();
            serviceCollection.TryAddSingleton<IWorkerLogProvider, WorkerLogProvider>();
        }
        
        public static void TryAddWorkerLogTransients(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<IWorkerLogFactory, WorkerLogFactory>();
            serviceCollection.TryAddTransient<IWorkerLogProvider, WorkerLogProvider>();
        }
        
        public static void TryAddWorkerLogScoped(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddScoped<IWorkerLogFactory, WorkerLogFactory>();
            serviceCollection.TryAddScoped<IWorkerLogProvider, WorkerLogProvider>();
        }
        
        public static void AddDbContext<TDbContext>(this IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey, string migrationAssembly)
            where TDbContext : DbContext
        {
            serviceCollection.AddDbContext<TDbContext>(options => 
                options.UseSqlServer(configuration.GetValue<string>(connectionStringKey), 
                    x => x.MigrationsAssembly(migrationAssembly)));
        }
        
        public static void AddDbContext<TDbContext>(this IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey)
            where TDbContext : DbContext
        {
            serviceCollection.AddDbContext<TDbContext>(options => 
                options.UseSqlServer(configuration.GetValue<string>(connectionStringKey)));
        }
        
        public static void TryAddTransientDbRepository<TDbContext>(this IServiceCollection serviceCollection)
            where TDbContext : DbContext
        {
            serviceCollection.TryAddTransient<IDbRepository, DbRepository<TDbContext>>();
        }
        
        public static void TryAddScopedDbRepository<TDbContext>(this IServiceCollection serviceCollection)
            where TDbContext : DbContext
        {
            serviceCollection.TryAddScoped<IScopedDbRepository, ScopedDbRepository<TDbContext>>();
        }
    }
}