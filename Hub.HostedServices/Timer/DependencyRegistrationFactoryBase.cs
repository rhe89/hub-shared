using Hub.HostedServices.Queue;
using Hub.HostedServices.Tasks;
using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Hub.Storage.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.HostedServices.Timer
{
    public abstract class DependencyRegistrationFactoryBase<TDbContext>
        where TDbContext : HubDbContext
    {
        public void AddBaseServices(IServiceCollection serviceCollection, 
            IConfiguration configuration, 
            string connectionStringKey)
        {
            serviceCollection.AddHostedService<TimerHostedService>();
            serviceCollection.AddDbContext<TDbContext>(configuration, connectionStringKey);
            serviceCollection.TryAddScopedDbRepository<TDbContext>();
            serviceCollection.TryAddSingleton<IBackgroundTaskCollection, BackgroundTaskCollection>();
            serviceCollection.AddSingleton<IBackgroundTask, WorkerLogMaintenanceBackgroundTask>();
            serviceCollection.TryAddHostedServiceFactoriesAsSingletons();
            serviceCollection.TryAddHostedServiceProvidersAsSingletons();
            
            AddDomainDependencies(serviceCollection, configuration);
        }
    
        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}