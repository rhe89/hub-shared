using System;
using System.IO;
using Hub.Logging;
using Hub.Web.DependencyRegistration;
using Hub.Web.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.Web.HostBuilder
{
    public static class ApiHostBuilder
    {
        public static IHostBuilder
           CreateHostBuilder<TStartup, TApiAppServiceCollectionBuilder, TDbContext> (string[] args) 
            where TApiAppServiceCollectionBuilder : ApiWithQueueHostedServiceDependencyRegistrationFactory<TDbContext>, new()
            where TStartup : ApiStartup<TDbContext, TApiAppServiceCollectionBuilder>
            where TDbContext : DbContext 
        { 
            var configPath = $"{Directory.GetCurrentDirectory()}/../..";

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", true)
                .Build();

            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                })
                .ConfigureAppConfiguration(b =>
                {
                    b.AddConfiguration(config);
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddHubLogger(new HubLoggerConfig
                    {
                        Color = ConsoleColor.Blue,
                        LogLevel = LogLevel.Information
                    });
                });
        }
    }
}