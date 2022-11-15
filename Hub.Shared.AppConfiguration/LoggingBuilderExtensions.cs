using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Logging.Console;

namespace Hub.Shared.AppConfiguration;

public static class LoggingBuilderExtensions
{
    public static ILoggingBuilder AddDefaultLoggingProviders(this ILoggingBuilder loggingBuilder)
    {
        static void AddLoggingFilters<TLoggerProvider>(ILoggingBuilder loggingBuilder)
            where TLoggerProvider : ILoggerProvider
        {
            loggingBuilder.AddFilter<TLoggerProvider>("", LogLevel.Information);
            loggingBuilder.AddFilter<TLoggerProvider>("Microsoft.EntityFrameworkCore.SqlServer", LogLevel.Warning);
            loggingBuilder.AddFilter<TLoggerProvider>("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
            loggingBuilder.AddFilter<TLoggerProvider>("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Warning);
        }
        
        loggingBuilder
            .ClearProviders()
            .AddConsole()
            .AddApplicationInsights();
        
        AddLoggingFilters<ConsoleLoggerProvider>(loggingBuilder);
        AddLoggingFilters<ApplicationInsightsLoggerProvider>(loggingBuilder);

        return loggingBuilder;
    }
}