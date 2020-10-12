using System.IO;
using Hub.Web.DependencyRegistration;
using Hub.Web.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Hub.Web.HostBuilder
{
    public class WebHostBuilder<TStartup, TWebAppServiceCollectionBuilder> 
        where TStartup : WebStartup<TWebAppServiceCollectionBuilder>
        where TWebAppServiceCollectionBuilder : WebDependencyRegistrationFactory, new()
    {
        protected static IHostBuilder CreateHostBuilder(string[] args)
        {
            var configPath = $"{Directory.GetCurrentDirectory()}/../..";
            
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", true)
                .Build();
            
            return Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(configurationBuilder => configurationBuilder.AddConfiguration(config))
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<TStartup>(); });
        }
    }
}