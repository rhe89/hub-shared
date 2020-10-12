using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Web.DependencyRegistration
{
    public abstract class WebDependencyRegistrationFactory
    {
        public void BuildServiceCollection(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            AddBlazor(serviceCollection, configuration);
            AddHttpClients(serviceCollection, configuration);
            AddServices(serviceCollection, configuration);
        }
        
        protected abstract void AddBlazor(IServiceCollection serviceCollection, IConfiguration configuration);
        protected abstract void AddHttpClients(IServiceCollection serviceCollection, IConfiguration configuration);
        protected abstract void AddServices(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}