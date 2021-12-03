using Hub.Shared.Storage.Azure;
using Hub.Shared.Storage.Repository;
using Hub.Shared.Storage.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Shared.HostedServices.ServiceBusQueue
{
    public abstract class DependencyRegistrationFactory<TDbContext>
        where TDbContext : HubDbContext
    {
        public void AddServices(IServiceCollection serviceCollection,
            IConfiguration configuration,
            string dbConnectionStringKey)
        {
            serviceCollection.AddDatabase<TDbContext>(configuration, dbConnectionStringKey);
            serviceCollection.AddTransient<ITableStorage, TableStorage>(x => 
                new TableStorage(configuration.GetValue<string>("STORAGE_ACCOUNT")));
            serviceCollection.TryAddTransient<IQueueProcessor, QueueProcessor>();
            serviceCollection.AddApplicationInsightsTelemetryWorkerService();
            
            AddDomainDependencies(serviceCollection, configuration);
            AddQueueListenerServices(serviceCollection, configuration);
        }
    
        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
        protected abstract void AddQueueListenerServices(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}