using Hub.Shared.Storage.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Shared.Web.BlazorServer
{
    public abstract class DependencyRegistrationFactory
    {
        public void BuildServiceCollection(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRazorPages();
            serviceCollection.AddServerSideBlazor();
            AddBlazorExtras(serviceCollection, configuration);
            AddHttpClients(serviceCollection, configuration);
            AddDomainDependencies(serviceCollection, configuration);
            
            serviceCollection.AddTransient<ITableStorage, TableStorage>(x => 
                new TableStorage(configuration.GetValue<string>("STORAGE_ACCOUNT")));
        }
        
        protected abstract void AddBlazorExtras(IServiceCollection serviceCollection, IConfiguration configuration);
        protected abstract void AddHttpClients(IServiceCollection serviceCollection, IConfiguration configuration);
        protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}