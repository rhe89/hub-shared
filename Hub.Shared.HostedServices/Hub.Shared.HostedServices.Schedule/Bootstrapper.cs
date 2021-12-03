using System.IO;
using Hub.Shared.Logging;
using Hub.Shared.Storage.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.HostedServices.Schedule
{
    public class Bootstrapper<TDependencyRegistrationFactory, TDbContext>
        where TDependencyRegistrationFactory : DependencyRegistrationFactory<TDbContext>, new()
        where TDbContext : HubDbContext
    {
        private readonly string[] _args;
        private readonly string _connectionStringKey;
        private IConfigurationRoot _config;

        public Bootstrapper(string [] args, string connectionStringKey)
        {
            _args = args;
            _connectionStringKey = connectionStringKey;
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
            new TDependencyRegistrationFactory().AddServices(serviceCollection, _config, _connectionStringKey);
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