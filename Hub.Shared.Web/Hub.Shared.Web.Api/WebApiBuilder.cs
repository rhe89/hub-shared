using System;
using System.Reflection;
using Hub.Shared.AppConfiguration;
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
        builder.Services.AddDatabase<TDbContext>(builder.Configuration, connectionStringKey);

        AddServices(builder.Services, builder.Configuration);

        return builder;
    }
    
    public static WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Logging.AddDefaultLoggingProviders();
        builder.Configuration.AddDefaultConfiguration();
        
        builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions { ConnectionString = builder.Configuration.GetValue<string>("AI_CONNECTION_STRING") });

        AddServices(builder.Services, builder.Configuration);

        return builder;
    }

    private static void AddServices(IServiceCollection serviceCollection, IConfiguration configuration)
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