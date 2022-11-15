using System;
using Hub.Shared.AppConfiguration;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Configuration;
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
        builder.Logging.AddDefaultLoggingProviders();
        builder.Configuration.AddDefaultConfiguration();
        builder.Services.AddApplicationInsightsTelemetry(new ApplicationInsightsServiceOptions { ConnectionString = builder.Configuration.GetValue<string>("AI_CONNECTION_STRING") });
        StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
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