using System;
using Hub.Shared.Configuration;
using Hub.Shared.Storage.Repository;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.HostedServices.Schedule;

[UsedImplicitly]
public static class ScheduledHostBuilder
{
    [UsedImplicitly]
    public static IHostBuilder CreateHostBuilder<TDbContext>(string [] args, 
        string connectionStringKey)
        where TDbContext : HubDbContext
    {
        return Host.CreateDefaultBuilder(args)
            .GetDefaultHostBuilder()
            .ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                serviceCollection.AddHostedService<HostedService>();
                serviceCollection.AddDatabase<TDbContext>(hostBuilderContext.Configuration, connectionStringKey);
                serviceCollection.TryAddSingleton<IScheduledCommandCollection, ScheduledCommandCollection>();
                serviceCollection.AddApplicationInsightsTelemetryWorkerService();
                serviceCollection.AddSingleton<ITelemetryInitializer>(
                    new CloudRoleNameInitializer(Environment.GetEnvironmentVariable("CLOUD_ROLE_NAME")));
            });
    }
}