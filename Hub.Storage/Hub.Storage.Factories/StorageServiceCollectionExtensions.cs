using Hub.Storage.Core.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Storage.Factories
{
    public static class StorageServiceCollectionExtensions
    {
        public static void TryAddHubFactoriesAsSingletons(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<ISettingFactory, SettingFactory>();
        }
        
        public static void TryAddHostedServiceFactoriesAsSingletons(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddHubFactoriesAsSingletons();
            serviceCollection.TryAddSingleton<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddSingleton<IWorkerLogFactory, WorkerLogFactory>();
        }
        
        public static void TryAddHubFactoriesAsTransients(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<ISettingFactory, SettingFactory>();
        }
        
        public static void TryAddHostedServiceFactoriesAsTransients(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddHubFactoriesAsTransients();
            serviceCollection.TryAddTransient<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddTransient<IWorkerLogFactory, WorkerLogFactory>();
        }
        
        public static void TryAddHubFactoriesAsScoped(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddScoped<ISettingFactory, SettingFactory>();
        }
        
        public static void TryAddHostedServiceFactoriesAsScoped(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddHubFactoriesAsScoped();
            serviceCollection.TryAddScoped<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddScoped<IWorkerLogFactory, WorkerLogFactory>();
        }
    }
}