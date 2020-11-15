using System;
using System.Reflection;
using Hub.HostedServices.Queue;
using Hub.Storage.Factories;
using Hub.Storage.Providers;
using Hub.Storage.Repository;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            serviceCollection.TryAddHubFactoriesAsTransients();
            serviceCollection.TryAddHubProvidersAsTransients();
            serviceCollection.AddQueueHostedService<TDbContext>();
        }
    }
}