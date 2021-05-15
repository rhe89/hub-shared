using Hub.HostedServices.Commands.Logging;
using Hub.HostedServices.Commands.Logging.Core;
using Hub.HostedServices.Schedule.Commands;
using Hub.Storage.Azure;
using Hub.Storage.Repository;
using Hub.Storage.Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.HostedServices.Schedule
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
            serviceCollection.TryAddSingleton<ICommandLogFactory, CommandLogFactory>();
        }

        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}