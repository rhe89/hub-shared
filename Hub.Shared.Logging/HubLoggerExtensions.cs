using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Logging.Console;

namespace Hub.Shared.Logging
{
    [UsedImplicitly]
    public static class HubLoggerExtensions
    {
        [UsedImplicitly]
        public static ILoggingBuilder AddHubLogging(this ILoggingBuilder loggingBuilder)
        {
            loggingBuilder.ClearProviders();

            loggingBuilder.AddConsole();
            loggingBuilder.AddFilters<ConsoleLoggerProvider>();
            
            loggingBuilder.AddApplicationInsights();
            loggingBuilder.AddFilters<ApplicationInsightsLoggerProvider>();

            return loggingBuilder;
        }

        private static void AddFilters<TLoggerProvider>(this ILoggingBuilder loggingBuilder)
            where TLoggerProvider : ILoggerProvider
        {
            loggingBuilder.AddFilter<TLoggerProvider>("", LogLevel.Information);
            loggingBuilder.AddFilter<TLoggerProvider>("Microsoft.EntityFrameworkCore.SqlServer", LogLevel.Warning);
            loggingBuilder.AddFilter<TLoggerProvider>("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            loggingBuilder.AddFilter<TLoggerProvider>("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
        }
    }
}
