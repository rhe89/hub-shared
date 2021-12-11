using Hub.Shared.Logging;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Shared.Web.BlazorServer;

[UsedImplicitly]
public abstract class DependencyRegistrationFactory
{
    [UsedImplicitly]
    public void AddServices(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddRazorPages();
        serviceCollection.AddServerSideBlazor();
        AddBlazorExtras(serviceCollection, configuration);
        AddHttpClients(serviceCollection, configuration);
        AddDomainDependencies(serviceCollection, configuration);
            
        serviceCollection.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
        {
            ConnectionString = configuration.GetValue<string>("AI_CONNECTION_STRING")
        });
            
        serviceCollection.AddLogging(loggingBuilder => loggingBuilder.AddHubLogging());
    }
        
    [UsedImplicitly]
    protected abstract void AddBlazorExtras(IServiceCollection serviceCollection, IConfiguration configuration);
        
    [UsedImplicitly]
    protected abstract void AddHttpClients(IServiceCollection serviceCollection, IConfiguration configuration);
        
    [UsedImplicitly]
    protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);
}