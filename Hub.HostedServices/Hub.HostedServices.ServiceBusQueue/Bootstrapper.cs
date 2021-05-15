using System.IO;
using Hub.Logging;
using Hub.Storage.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.ServiceBusQueue
{
    public class Bootstrapper<TDependencyRegistrationFactory, TDbContext>
        where TDependencyRegistrationFactory : DependencyRegistrationFactory<TDbContext>, new()
        where TDbContext : HubDbContext
    {
        private readonly string[] _args;
        private readonly string _dbConnectionStringKey;
        private IConfigurationRoot _config;

        public Bootstrapper(string [] args, string dbConnectionStringKey)
        {
            _args = args;
            _dbConnectionStringKey = dbConnectionStringKey;
        }
        
        public IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder(_args)
                .ConfigureHostConfiguration(AddConfiguration)
                .ConfigureServices(RegisterServices)
                .ConfigureLogging(AddLogging);
        }
        
        private void AddConfiguration(IConfigurationBuilder configurationBuilder)
        {
            var configPath = $"{Directory.GetCurrentDirectory()}/../..";

            _config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", true)
                .Build();

             configurationBuilder.AddConfiguration(_config);
        }

        private void RegisterServices(IServiceCollection serviceCollection)
        {
            new TDependencyRegistrationFactory().AddServices(serviceCollection, _config, _dbConnectionStringKey);
        }
        
        private void AddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddHubLogger(new HubLoggerConfig
            {
                Color = System.ConsoleColor.Blue,
                LogLevel = LogLevel.Information
            }, _config);
        }
    }
}