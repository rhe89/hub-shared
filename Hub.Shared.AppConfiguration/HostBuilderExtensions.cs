using Microsoft.Extensions.Hosting;

namespace Hub.Shared.AppConfiguration;

public static class HostBuilderExtensions
{
    public static IHostBuilder GetDefaultHostBuilder(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureAppConfiguration(configurationBuilder => configurationBuilder.AddDefaultConfiguration())
            .ConfigureLogging(loggingBuilder => loggingBuilder.AddDefaultLoggingProviders());
    }
}