using System.IO;
using Hub.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.Timer
{
    public abstract class TimerHostBuilder<TDbContext>
        where TDbContext : DbContext
    {
        private readonly string[] _args;
        private readonly string _connectionStringKey;
        private IConfigurationRoot _config;

        protected TimerHostBuilder(string [] args, string connectionStringKey)
        {
            _args = args;
            _connectionStringKey = connectionStringKey;
        }
        
        public IHost Build()
        {
            return Host.CreateDefaultBuilder(_args)
                .ConfigureHostConfiguration(AddConfiguration)
                .ConfigureServices(RegisterServices)
                .ConfigureLogging(AddLogging)
                .Build();
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

        private void RegisterServices(HostBuilderContext hostBuilderContext, IServiceCollection serviceCollection)
        {
            serviceCollection.AddTimerHostedService<TDbContext>(_config, _connectionStringKey);
            
            RegisterDomainDependencies(serviceCollection, _config);
        }

        protected abstract void RegisterDomainDependencies(IServiceCollection serviceCollection, IConfiguration configuration);

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