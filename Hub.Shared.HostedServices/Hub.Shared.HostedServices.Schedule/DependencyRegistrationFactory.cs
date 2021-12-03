using Hub.Shared.HostedServices.Commands;
using Hub.Shared.Storage.Azure;
using Hub.Shared.Storage.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Shared.HostedServices.Schedule
{
    public abstract class DependencyRegistrationFactory<TDbContext>
        where TDbContext : HubDbContext
    {
        public void AddServices(IServiceCollection serviceCollection, 
            IConfiguration configuration, 
            string connectionStringKey)
        {
            AddDomainDependencies(serviceCollection, configuration);

            serviceCollection.AddHostedService<HostedService>();
            serviceCollection.AddDatabase<TDbContext>(configuration, connectionStringKey);
            serviceCollection.AddSingleton<ITableStorage, TableStorage>(x => 
                new TableStorage(configuration.GetValue<string>("STORAGE_ACCOUNT")));
            serviceCollection.TryAddSingleton<IScheduledCommandCollection, ScheduledCommandCollection>();
            serviceCollection.AddApplicationInsightsTelemetryWorkerService();
        }

        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}