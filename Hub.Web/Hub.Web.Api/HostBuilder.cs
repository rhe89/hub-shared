using System;
using System.IO;
using Hub.Logging;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.Web.Api
{
    public static class HostBuilder<TDependencyRegistrationFactory, TDbContext>
        where TDependencyRegistrationFactory : DependencyRegistrationFactoryBase<TDbContext>, new()
        where TDbContext : HubDbContext 
    {
        public static IHostBuilder Create(string[] args) 
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
                    webBuilder.UseStartup<Startup<TDependencyRegistrationFactory, TDbContext>>();
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