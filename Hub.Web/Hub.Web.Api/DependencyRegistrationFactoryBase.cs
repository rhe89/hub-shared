using System.Reflection;
using Hub.HostedServices.QueueHost;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Providers;
using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Hub.Storage.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Web.Api
{
    public abstract class DependencyRegistrationFactoryBase
    {
        public void AddBaseServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            
            var part = new AssemblyPart(entryAssembly);
            
            serviceCollection.AddControllers()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));
            
            AddDomainDependencies(serviceCollection, configuration);
        }
    
        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
    }
    
    public abstract class DependencyRegistrationFactoryBase<TDbContext> : DependencyRegistrationFactoryBase
        where TDbContext : HubDbContext 
    {
        private readonly string _connectionStringKey;
        private readonly string _migrationAssembly;

        protected DependencyRegistrationFactoryBase(string connectionStringKey, string migrationAssembly)
        {
            _connectionStringKey = connectionStringKey;
            _migrationAssembly = migrationAssembly;
        }
        
        public new void AddBaseServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            base.AddBaseServices(serviceCollection, configuration);
            
            serviceCollection.AddDbContext<TDbContext>(configuration, _connectionStringKey, _migrationAssembly);
            serviceCollection.TryAddTransientDbRepository<TDbContext>();
            serviceCollection.TryAddTransient<ISettingFactory, SettingFactory>();
            serviceCollection.TryAddTransient<IBackgroundTaskConfigurationFactory, BackgroundTaskConfigurationFactory>();
            serviceCollection.TryAddTransient<IWorkerLogFactory, WorkerLogFactory>();
            serviceCollection.TryAddTransient<ISettingProvider, SettingProvider>();
            serviceCollection.TryAddTransient<IBackgroundTaskConfigurationProvider, BackgroundTaskConfigurationProvider>();
            serviceCollection.TryAddTransient<IWorkerLogProvider, WorkerLogProvider>();
            serviceCollection.AddQueueHostedService<TDbContext>();
        }
    }
}