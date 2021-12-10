using System;
using Azure.Identity;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.Web.BlazorServer
{
    [UsedImplicitly]
    public static class HostBuilder<TStartup> 
        where TStartup : class
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
                .ConfigureHostConfiguration(configurationBuilder => configurationBuilder.AddConfiguration(config))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<TStartup>());
        }
    }
}