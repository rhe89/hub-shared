using Hub.Storage.Core.Providers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Storage.Providers
{
    public static class StorageServiceCollectionExtensions
    {
        public static void TryAddHubProvidersAsSingletons(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<ISettingProvider, SettingProvider>();
        }
        
        public static void TryAddHostedServiceProvidersAsSingletons(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddHubProvidersAsSingletons();
            serviceCollection.TryAddSingleton<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
            serviceCollection.TryAddSingleton<IWorkerLogProvider, WorkerLogProvider>();
        }
        
        public static void TryAddHubProvidersAsTransients(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddTransient<ISettingProvider, SettingProvider>();
        }
        
        public static void TryAddHostedServiceProvidersAsTransients(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddHubProvidersAsTransients();
            serviceCollection.TryAddTransient<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
            serviceCollection.TryAddTransient<IWorkerLogProvider, WorkerLogProvider>();
        }
        
        public static void TryAddHubProvidersAsScoped(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddScoped<ISettingProvider, SettingProvider>();
        }
        
        public static void TryAddHostedServiceProvidersAsScoped(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddHubProvidersAsScoped();
            serviceCollection.TryAddScoped<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
            serviceCollection.TryAddScoped<IWorkerLogProvider, WorkerLogProvider>();
        }
    }
}