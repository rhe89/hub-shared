using System.IO;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Hub.Web.BlazorServer
{
    public static class HostBuilder<TDependencyRegistrationFactory> 
        where TDependencyRegistrationFactory : DependencyRegistrationFactoryBase, new()
    {
        public static IHostBuilder Create(string[] args)
        {
            var configPath = $"{Directory.GetCurrentDirectory()}/../..";
            
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", true)
                .Build();
            
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configurationBuilder => configurationBuilder.AddConfiguration(configuration))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup<TDependencyRegistrationFactory>>());
        }
    }
    
    public static class HostBuilder<TDependencyRegistrationFactory, TDbContext>
        where TDependencyRegistrationFactory : DependencyRegistrationFactoryBase<TDbContext>, new()
        where TDbContext : HubDbContext
    {

        public static IHostBuilder Create(string[] args)
        {
            var configPath = $"{Directory.GetCurrentDirectory()}/../..";
            
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", true)
                .Build();
            
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configurationBuilder => configurationBuilder.AddConfiguration(configuration))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup<TDependencyRegistrationFactory>>());
        }
    }
}