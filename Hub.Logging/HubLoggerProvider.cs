using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace Hub.Logging
{
    public class HubLoggerProvider : ILoggerProvider
    {
        private readonly HubLoggerConfig _config;
        private readonly ConcurrentDictionary<string, HubLogger> _loggers = new ConcurrentDictionary<string, HubLogger>();

        public HubLoggerProvider(HubLoggerConfig config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new HubLogger(name, _config));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
