using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Hub.Web.BlazorServer
{
    public static class HostBuilder<TStartup> 
        where TStartup : class
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
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<TStartup>());
        }
    }
}