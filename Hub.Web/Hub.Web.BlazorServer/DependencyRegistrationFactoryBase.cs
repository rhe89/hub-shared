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
}