using Hub.HostedServices.Tasks;
using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Hub.Storage.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.HostedServices.Queue
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQueueHostedService<TDbContext>(this IServiceCollection serviceCollection)
            where TDbContext : HubDbContext
        {
            serviceCollection.AddHostedService<QueuedHostedService>();
            serviceCollection.TryAddScopedDbRepository<TDbContext>();
            serviceCollection.TryAddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            serviceCollection.TryAddSingleton<IBackgroundTaskQueueHandler, BackgroundTaskQueueHandler>();
            serviceCollection.TryAddHostedServiceFactoriesAsSingletons();
            serviceCollection.TryAddHostedServiceProvidersAsSingletons();
        }
    }
}