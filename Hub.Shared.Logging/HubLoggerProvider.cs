using System.Collections.Concurrent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.Logging
{
    public class HubLoggerProvider : ILoggerProvider
    {
        private readonly HubLoggerConfig _loggerConfig;
        private readonly IConfiguration _appConfig;
        private readonly ConcurrentDictionary<string, HubLogger> _loggers = new ConcurrentDictionary<string, HubLogger>();

        public HubLoggerProvider(HubLoggerConfig loggerConfig, IConfiguration appConfig)
        {
            _loggerConfig = loggerConfig;
            _appConfig = appConfig;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new HubLogger(name, _loggerConfig, _appConfig));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
