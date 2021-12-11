using System.Reflection;
using Hub.Shared.Storage.Repository;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Shared.Web.Api;

public abstract class DependencyRegistrationFactory<TDbContext>
    where TDbContext : HubDbContext 
{
    private readonly string _connectionStringKey;
    private readonly string _migrationAssembly;

    protected DependencyRegistrationFactory(string connectionStringKey, string migrationAssembly)
    {
        _connectionStringKey = connectionStringKey;
        _migrationAssembly = migrationAssembly;
    }
        
    public void AddServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var entryAssembly = Assembly.GetEntryAssembly();
            
        var part = new AssemblyPart(entryAssembly);
            
        serviceCollection.AddControllers()
            .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));
            
        serviceCollection.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
        {
            ConnectionString = configuration.GetValue<string>("AI_CONNECTION_STRING")
        });
            
        serviceCollection.AddDatabase<TDbContext>(configuration, _connectionStringKey, _migrationAssembly);
            
        AddDomainDependencies(serviceCollection, configuration);
    }
        
    protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, [UsedImplicitly]IConfiguration configuration);
}