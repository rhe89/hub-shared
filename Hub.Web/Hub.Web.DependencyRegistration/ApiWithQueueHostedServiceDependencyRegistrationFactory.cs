using Hub.HostedServices.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Web.DependencyRegistration
{
    public abstract class ApiWithQueueHostedServiceDependencyRegistrationFactory<TDbContext> : ApiDependencyRegistrationFactory<TDbContext>
        where TDbContext : DbContext
    {
        protected ApiWithQueueHostedServiceDependencyRegistrationFactory(string connectionStringKey, string migrationAssembly)  : base(connectionStringKey, migrationAssembly)
        {
        }
        
        public override void BuildServiceCollection(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            base.BuildServiceCollection(serviceCollection, configuration);

            serviceCollection.AddQueueHostedService<TDbContext>();
            RegisterDomainDependenciesForQueueHostedService(serviceCollection, configuration);
            RegisterSharedDomainDependencies(serviceCollection, configuration);
        }
        
        protected abstract void RegisterDomainDependenciesForQueueHostedService(IServiceCollection serviceCollection, IConfiguration configuration);
        
        protected abstract void RegisterSharedDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);

    }
}