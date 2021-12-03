using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.Logging
{
    public static class HubLoggerExtensions
    {
        public static ILoggingBuilder AddHubLogger(this ILoggingBuilder loggingBuilder, HubLoggerConfig loggerConfig, IConfiguration appConfig)
        {
            loggingBuilder.ClearProviders();

            loggingBuilder.AddProvider(new HubLoggerProvider(loggerConfig, appConfig));

            return loggingBuilder;
        }
        public static ILoggingBuilder AddHubLogger(this ILoggingBuilder loggingBuilder, IConfiguration appConfig)
        {
            loggingBuilder.ClearProviders();

            var loggerConfig = new HubLoggerConfig();

            return loggingBuilder.AddHubLogger(loggerConfig, appConfig);
        }
        public static ILoggingBuilder AddHubLogger(this ILoggingBuilder loggingBuilder, Action<HubLoggerConfig> configure, IConfiguration appConfig)
        {
            var loggerConfig = new HubLoggerConfig();

            configure(loggerConfig);

            loggingBuilder.ClearProviders();

            return loggingBuilder.AddHubLogger(loggerConfig, appConfig);
        }
    }
}
