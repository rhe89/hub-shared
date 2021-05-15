using System;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.Logging
{
    public class LogItem : TableEntity
    {

        public LogItem()
        {
            
        }

        public LogItem(string partitionKey, string logMessage)
        {
            LogMessage = logMessage;
            PartitionKey = partitionKey;
            RowKey = DateTime.Now.ToString("O");
            Timestamp = DateTimeOffset.Now;
            LogMessage = logMessage;
        }
        
        public string LogMessage { get; set; }

    }
}