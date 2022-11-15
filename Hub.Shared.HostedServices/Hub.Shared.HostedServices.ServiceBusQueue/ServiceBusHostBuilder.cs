using System;
using Hub.Shared.AppConfiguration;
using Hub.Shared.Storage.Repository;
using Hub.Shared.Storage.ServiceBus;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.HostedServices.ServiceBusQueue;

[UsedImplicitly]
public static class ServiceBusHostBuilder
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
                serviceCollection.AddDatabase<TDbContext>(hostBuilderContext.Configuration, connectionStringKey);
                serviceCollection.TryAddTransient<IQueueProcessor, QueueProcessor>();
                serviceCollection.AddApplicationInsightsTelemetryWorkerService();
                serviceCollection.AddSingleton<ITelemetryInitializer>(new CloudRoleNameInitializer(Environment.GetEnvironmentVariable("CLOUD_ROLE_NAME")));
            });
    }
}