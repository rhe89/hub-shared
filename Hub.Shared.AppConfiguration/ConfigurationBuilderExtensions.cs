using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;

namespace Hub.Shared.AppConfiguration;

public static class ConfigurationBuilderExtensions
{
    public static IConfigurationBuilder AddDefaultConfiguration(this IConfigurationBuilder configurationBuilder)
    {
        return configurationBuilder
            .AddEnvironmentVariables()
            .AddAzureAppConfiguration(options =>
            {
                options
                    .Connect(Environment.GetEnvironmentVariable("APP_CONFIG_CONNECTION_STRING"))
                    .Select(KeyFilter.Any)
                    .Select(KeyFilter.Any, Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                    .ConfigureKeyVault(keyVaultOptions =>
                        keyVaultOptions.SetCredential(new DefaultAzureCredential()))
                    .ConfigureRefresh(refreshOptions =>
                        refreshOptions.Register(key: "Sentinel", refreshAll: true));
            });
    }
}