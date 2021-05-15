using Hub.HostedServices.Commands.Logging;
using Hub.HostedServices.Commands.Logging.Core;
using Hub.ServiceBus;
using Hub.ServiceBus.Core;
using Hub.Storage.Azure;
using Hub.Storage.Azure.Core;
using Hub.Storage.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.HostedServices.ServiceBusQueue
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
            serviceCollection.TryAddTransient<ICommandLogFactory, CommandLogFactory>();
            serviceCollection.TryAddTransient<IQueueProcessor, QueueProcessor>();
            
            AddDomainDependencies(serviceCollection, configuration);
            AddQueueListenerServices(serviceCollection, configuration);
        }
    
        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
        
        protected abstract void AddQueueListenerServices(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}