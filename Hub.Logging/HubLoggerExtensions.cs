using System;
using Microsoft.Extensions.Logging;

namespace Hub.Logging
{
    public static class HubLoggerExtensions
    {
        public static ILoggingBuilder AddHubLogger(this ILoggingBuilder loggingBuilder, HubLoggerConfig config)
        {
            loggingBuilder.ClearProviders();

            loggingBuilder.AddProvider(new HubLoggerProvider(config));

            return loggingBuilder;
        }
        public static ILoggingBuilder AddHubLogger(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();

            var config = new HubLoggerConfig();

            return loggingBuilder.AddHubLogger(config);
        }
        public static ILoggingBuilder AddHubLogger(this ILoggingBuilder loggingBuilder, Action<HubLoggerConfig> configure)
        {
            var config = new HubLoggerConfig();

            configure(config);

            loggingBuilder.ClearProviders();

            return loggingBuilder.AddHubLogger(config);
        }
    }
}
