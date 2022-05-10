using System;
using System.Reflection;
using Hub.Shared.Configuration;
using Hub.Shared.Storage.Repository;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.Web.Api;

[UsedImplicitly]
public static class WebApiBuilder
{
    [UsedImplicitly]
    public static WebApplicationBuilder CreateWebApplicationBuilder<TDbContext>(string[] args, string connectionStringKey)
        where TDbContext : HubDbContext
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Logging.AddDefaultLoggingProviders();
        builder.Configuration.AddDefaultConfiguration();
        
        builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions { ConnectionString = builder.Configuration.GetValue<string>("AI_CONNECTION_STRING") });

        AddServices<TDbContext>(builder.Services, builder.Configuration, connectionStringKey);

        return builder;
    }

    private static void AddServices<TDbContext>(IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey)
        where TDbContext : HubDbContext
    {
        var entryAssembly = Assembly.GetEntryAssembly();

        if (entryAssembly != null)
        {
            var part = new AssemblyPart(entryAssembly);
            
            serviceCollection.AddControllers()
                .ConfigureApplicationPartManager(apm => apm.ApplicationParts.Add(part));
        }

        serviceCollection.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions
        {
            ConnectionString = configuration.GetValue<string>("AI_CONNECTION_STRING")
        });
        
        serviceCollection.AddSingleton<ITelemetryInitializer>(new CloudRoleNameInitializer(Environment.GetEnvironmentVariable("CLOUD_ROLE_NAME")));

        serviceCollection.AddDatabase<TDbContext>(configuration, connectionStringKey);
    }

    [UsedImplicitly]
    public static WebApplication BuildApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

        app.UseHttpsRedirection();
        
        app.MapControllers();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseRouting();
        app.UseCors();
        
        return app;
    }
}