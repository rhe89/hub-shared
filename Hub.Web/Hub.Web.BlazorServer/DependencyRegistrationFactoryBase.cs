using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hub.Storage.Repository.DatabaseContext;
using Hub.Storage.Repository;

namespace Hub.Web.BlazorServer
{
    public abstract class DependencyRegistrationFactoryBase
    {
        public void BuildServiceCollection(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRazorPages();
            serviceCollection.AddServerSideBlazor();
            AddBlazorExtras(serviceCollection, configuration);
            AddHttpClients(serviceCollection, configuration);
            AddDomainDependencies(serviceCollection, configuration);
        }
        
        protected abstract void AddBlazorExtras(IServiceCollection serviceCollection, IConfiguration configuration);
        protected abstract void AddHttpClients(IServiceCollection serviceCollection, IConfiguration configuration);
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

        public new void BuildServiceCollection(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            base.BuildServiceCollection(serviceCollection, configuration);

            serviceCollection.AddDbContext<TDbContext>(configuration, _connectionStringKey, _migrationAssembly);
        }
    }
}