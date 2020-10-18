using System;
using Microsoft.Extensions.Logging;

namespace Hub.Logging
{
    public class HubLogger : ILogger
    {
        private static readonly object Lock = new object(); 
        private readonly HubLoggerConfig _config;
        private readonly string _name;

        public HubLogger(string name, HubLoggerConfig config) => (_name, _config) = (name, config);

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            lock (Lock)
            {
                return (int)logLevel >= (int)_config.LogLevel;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel) || Ignore(logLevel, _name))
            {
                return;
            }

            lock (Lock)
            {
                if (_config.EventId != 0 && _config.EventId != eventId.Id) return;
                
                var color = Console.ForegroundColor;

                Console.ForegroundColor = GetColor(logLevel);

                var msg = GetMessage(logLevel, state, exception, formatter);
                
                Console.WriteLine(msg);

                Console.ForegroundColor = color;
            }
        }
        
        private static bool Ignore(LogLevel logLevel, string name)
            => (name, logLevel) switch
            {
                ("Microsoft.EntityFrameworkCore.Hub.Storage.Command", LogLevel.Information) => true,
                ("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information) => true,
                ("Microsoft.EntityFrameworkCore.Infrastructure", LogLevel.Information) => true,
                _ => false
            };

        private ConsoleColor GetColor(LogLevel logLevel)
            => logLevel switch
            {
                LogLevel.Information => _config.Color,
                LogLevel.Warning => ConsoleColor.DarkYellow,
                LogLevel.Error => ConsoleColor.Red,
                _ => _config.Color
            };

        private string GetMessage<TState>(LogLevel logLevel, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
            => exception == null
                ? GetStandardMessage(logLevel, state, null, formatter)
                : GetExceptionMessage(logLevel, state, exception, formatter);

        private string GetStandardMessage<TState>(LogLevel logLevel, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] [{logLevel.ToString().ToUpperInvariant()}] [{_name}] {formatter(state, exception)}";
        
        private string GetExceptionMessage<TState>(LogLevel logLevel, TState state, Exception exception, Func<TState, Exception, string> formatter)
            =>
                $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] [{logLevel.ToString().ToUpperInvariant()}] [{_name}]" +
                $"\n[EXCEPTION]       {exception.Message}" +
                $"\n[INNER EXCEPTION] {(exception.InnerException is null ? "(none)" : formatter(state, exception.InnerException))}" +
                $"\n[STACKTRACE]   {exception.StackTrace}";
    }
}
