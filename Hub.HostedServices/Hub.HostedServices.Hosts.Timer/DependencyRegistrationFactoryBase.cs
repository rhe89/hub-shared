using Hub.HostedServices.Core.Tasks;
using Hub.HostedServices.Tasks;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Providers;
using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Hub.Storage.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.HostedServices.Hosts.Timer
{
    public abstract class DependencyRegistrationFactoryBase<TDbContext>
        where TDbContext : HostedServiceDbContext
    {
        public void AddServices(IServiceCollection serviceCollection, 
            IConfiguration configuration, 
            string connectionStringKey)
        {
            serviceCollection.AddHostedService<TimerHostedService>();
            serviceCollection.AddDbContext<TDbContext>(configuration, connectionStringKey);
            serviceCollection.TryAddSingleton<IBackgroundTaskCollection, BackgroundTaskCollection>();
            serviceCollection.AddSingleton<IBackgroundTask, WorkerLogMaintenanceBackgroundTask>();
            serviceCollection.TryAddSingleton<ISettingFactory, SettingFactory>();
            serviceCollection.TryAddSingleton<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddSingleton<IWorkerLogFactory, WorkerLogFactory>();
            serviceCollection.TryAddSingleton<ISettingProvider, SettingProvider>();
            serviceCollection.TryAddSingleton<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
            serviceCollection.TryAddSingleton<IWorkerLogProvider, WorkerLogProvider>();
            
            AddDomainDependencies(serviceCollection, configuration);
        }
    
        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}