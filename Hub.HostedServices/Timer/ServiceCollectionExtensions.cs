using Hub.HostedServices.Tasks;
using Hub.Storage.DependencyRegistration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.HostedServices.Timer
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTimerHostedService<TDbContext>(this IServiceCollection serviceCollection, 
            IConfiguration configuration, 
            string connectionStringKey)
            where TDbContext : DbContext
        {
            serviceCollection.AddHostedService<TimerHostedService>();
            serviceCollection.AddDbContext<TDbContext>(configuration, connectionStringKey);
            serviceCollection.TryAddScopedDbRepository<TDbContext>();
            serviceCollection.TryAddSingleton<IBackgroundTaskCollection, BackgroundTaskCollection>();
            serviceCollection.AddSingleton<IBackgroundTask, WorkerLogMaintenanceBackgroundTask>();
            serviceCollection.TryAddWorkerLogSingletons();
            serviceCollection.TryAddSettingSingletons();
            serviceCollection.TryAddBackgroundTaskConfigurationSingletons();
        }
        
    }
}