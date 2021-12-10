using System;
using Azure.Identity;
using Hub.Shared.Logging;
using Hub.Shared.Storage.Repository;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.Web.Api
{
    [UsedImplicitly]
    public static class HostBuilder<TDependencyRegistrationFactory, TDbContext>
        where TDependencyRegistrationFactory : DependencyRegistrationFactory<TDbContext>, new()
        where TDbContext : HubDbContext 
    {
        [UsedImplicitly]
        public static IHostBuilder Create(string[] args) 
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddAzureAppConfiguration(options =>
                {
                    options.Connect(Environment.GetEnvironmentVariable("APP_CONFIG_CONNECTION_STRING"))
                        .Select(KeyFilter.Any)
                        .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
                    options.ConfigureKeyVault(o =>
                        o.SetCredential(new DefaultAzureCredential()));
                })
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
                    loggingBuilder.AddHubLogging();
                });
        }
    }
}