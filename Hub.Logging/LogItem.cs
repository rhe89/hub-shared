using System;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.Logging
{
    public class LogItem : TableEntity
    {

        public LogItem()
        {
            
        }

        public LogItem(string logLevel, string domain, string logMessage)
        {
            var now = DateTime.Now;
            
            PartitionKey = DateTime.Now.ToString("yyyy-MM-dd");
            RowKey = now.ToString("HH:mm:ss.ffff");
            Timestamp = DateTimeOffset.Now;
            
            LogLevel = logLevel;
            Domain = domain;
            LogMessage = logMessage;
        }
        
        public string LogLevel { get; set; }
        public string Domain { get; set; }
        public string LogMessage { get; set; }
    }
}