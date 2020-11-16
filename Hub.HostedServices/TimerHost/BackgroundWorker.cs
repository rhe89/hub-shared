using System.IO;
using Hub.Logging;
using Hub.Storage.Repository.DatabaseContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.TimerHost
{
    public class BackgroundWorker<TDependencyRegistrationFactory, TDbContext>
        where TDependencyRegistrationFactory : DependencyRegistrationFactoryBase<TDbContext>, new()
        where TDbContext : HubDbContext
    {
        private readonly string[] _args;
        private readonly string _connectionStringKey;
        private IConfigurationRoot _config;

        public BackgroundWorker(string [] args, string connectionStringKey)
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
            new TDependencyRegistrationFactory().AddBaseServices(serviceCollection, _config, _connectionStringKey);
        }
        
        private static void AddLogging(ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.AddHubLogger(new HubLoggerConfig
            {
                Color = System.ConsoleColor.Blue,
                LogLevel = LogLevel.Information
            });
        }
    }
}