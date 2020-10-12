using Hub.Storage.DependencyRegistration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Web.DependencyRegistration
{
    public abstract class ApiDependencyRegistrationFactory<TDbContext>
        where TDbContext : DbContext 
    {
        private readonly string _connectionStringKey;
        private readonly string _migrationAssembly;

        protected ApiDependencyRegistrationFactory(string connectionStringKey, string migrationAssembly)
        {
            _connectionStringKey = connectionStringKey;
            _migrationAssembly = migrationAssembly;
        }
        
        public virtual void BuildServiceCollection(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddControllers();
            
            serviceCollection.AddDbContext<TDbContext>(configuration, _connectionStringKey, _migrationAssembly);
            serviceCollection.TryAddTransientDbRepository<TDbContext>();
            serviceCollection.TryAddSettingTransients();

            RegisterDomainDependenciesForApi(serviceCollection, configuration);
        }
    
        protected abstract void RegisterDomainDependenciesForApi(IServiceCollection serviceCollection, IConfiguration configuration);
    }
}