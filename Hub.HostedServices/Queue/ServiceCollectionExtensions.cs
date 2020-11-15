using Hub.HostedServices.Tasks;
using Hub.Storage.DependencyRegistration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.HostedServices.Queue
{
    public static class ServiceCollectionExtensions
    {
        public static void AddQueueHostedService<TDbContext>(this IServiceCollection serviceCollection)
            where TDbContext : DbContext
        {
            serviceCollection.AddHostedService<QueuedHostedService>();
            serviceCollection.TryAddScopedDbRepository<TDbContext>();
            serviceCollection.TryAddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            serviceCollection.TryAddSingleton<IBackgroundTaskQueueHandler, BackgroundTaskQueueHandler>();
            serviceCollection.TryAddBackgroundTaskConfigurationSingletons();
            serviceCollection.TryAddWorkerLogSingletons();
            serviceCollection.TryAddSettingSingletons();
        }
    }
}