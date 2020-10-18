using System;
using Microsoft.Extensions.Logging;

namespace Hub.Logging
{
    public class HubLoggerConfig
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; }
        public ConsoleColor Color { get; set; } = ConsoleColor.White;
    }
}