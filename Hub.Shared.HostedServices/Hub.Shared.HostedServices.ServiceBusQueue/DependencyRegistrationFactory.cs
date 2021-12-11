using Hub.Shared.Storage.Repository;
using Hub.Shared.Storage.ServiceBus;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Shared.HostedServices.ServiceBusQueue;

public abstract class DependencyRegistrationFactory<TDbContext>
    where TDbContext : HubDbContext
{
    public void AddServices(IServiceCollection serviceCollection,
        IConfiguration configuration,
        string dbConnectionStringKey)
    {
        serviceCollection.AddDatabase<TDbContext>(configuration, dbConnectionStringKey);
        serviceCollection.TryAddTransient<IQueueProcessor, QueueProcessor>();
        serviceCollection.AddApplicationInsightsTelemetryWorkerService();
            
        AddDomainDependencies(serviceCollection, configuration);
        AddQueueListenerServices(serviceCollection, configuration);
    }
    
    protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, [UsedImplicitly]IConfiguration configuration);
    protected abstract void AddQueueListenerServices(IServiceCollection serviceCollection, [UsedImplicitly]IConfiguration configuration);
}