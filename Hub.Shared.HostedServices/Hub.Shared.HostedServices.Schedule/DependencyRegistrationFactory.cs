using System;
using Hub.Shared.Logging;
using Hub.Shared.Storage.Repository;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Shared.HostedServices.Schedule;

public abstract class DependencyRegistrationFactory<TDbContext>
    where TDbContext : HubDbContext
{
    public void AddServices(IServiceCollection serviceCollection, 
        IConfiguration configuration, 
        string connectionStringKey)
    {
        AddDomainDependencies(serviceCollection, configuration);

        serviceCollection.AddHostedService<HostedService>();
        serviceCollection.AddDatabase<TDbContext>(configuration, connectionStringKey);
        serviceCollection.TryAddSingleton<IScheduledCommandCollection, ScheduledCommandCollection>();
        serviceCollection.AddApplicationInsightsTelemetryWorkerService();
        serviceCollection.AddSingleton<ITelemetryInitializer>(new CloudRoleNameInitializer(Environment.GetEnvironmentVariable("CLOUD_ROLE_NAME")));

    }

    protected abstract void AddDomainDependencies(IServiceCollection serviceCollection, [UsedImplicitly]IConfiguration configuration);
}