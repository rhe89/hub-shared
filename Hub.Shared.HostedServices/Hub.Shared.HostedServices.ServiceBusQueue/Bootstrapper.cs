using System;
using Azure.Identity;
using Hub.Shared.Logging;
using Hub.Shared.Storage.Repository;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.HostedServices.ServiceBusQueue;

[UsedImplicitly]
public class Bootstrapper<TDependencyRegistrationFactory, TDbContext>
    where TDependencyRegistrationFactory : DependencyRegistrationFactory<TDbContext>, new()
    where TDbContext : HubDbContext
{
    private readonly string[] _args;
    private readonly string _dbConnectionStringKey;
    private IConfigurationRoot _config;

    public Bootstrapper(string[] args, string dbConnectionStringKey)
    {
        _args = args;
        _dbConnectionStringKey = dbConnectionStringKey;
    }

    [UsedImplicitly]
    public IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder(_args)
            .ConfigureHostConfiguration(AddConfiguration)
            .ConfigureServices(RegisterServices)
            .ConfigureLogging(l => l.AddHubLogging());
    }

    private void AddConfiguration(IConfigurationBuilder configurationBuilder)
    {
        _config = configurationBuilder
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
    }

    private void RegisterServices(IServiceCollection serviceCollection)
    {
        new TDependencyRegistrationFactory().AddServices(serviceCollection, _config, _dbConnectionStringKey);
    }
}