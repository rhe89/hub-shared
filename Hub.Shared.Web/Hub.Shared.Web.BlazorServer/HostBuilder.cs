using System;
using Azure.Identity;
using Hub.Shared.Logging;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.Web.BlazorServer;

[UsedImplicitly]
public static class BlazorServerBuilder
{
    [UsedImplicitly]
    public static WebApplicationBuilder CreateWebApplicationBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddSingleton<ITelemetryInitializer>(new CloudRoleNameInitializer(Environment.GetEnvironmentVariable("CLOUD_ROLE_NAME")));
        builder.Logging.AddHubLogging();
        builder.Configuration.AddEnvironmentVariables();
        builder.Configuration.AddAzureAppConfiguration(options =>
        {
            options.Connect(Environment.GetEnvironmentVariable("APP_CONFIG_CONNECTION_STRING"))
                .Select(KeyFilter.Any)
                .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            options.ConfigureKeyVault(o =>
                o.SetCredential(new DefaultAzureCredential()));
        });
        builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions { ConnectionString = builder.Configuration.GetValue<string>("AI_CONNECTION_STRING") });
        
        return builder;
    }
    
    [UsedImplicitly]
    public static WebApplication BuildApp(this WebApplicationBuilder builder)
    {
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");
        
        return app;
    }
}