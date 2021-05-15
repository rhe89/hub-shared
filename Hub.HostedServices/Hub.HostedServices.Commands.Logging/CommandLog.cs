using System;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.HostedServices.Commands.Logging
{
    public class CommandLog : TableEntity
    {
        public CommandLog()
        {
            
        }
        
        public CommandLog(string commandName)
        {
            PartitionKey = commandName;
            RowKey = DateTime.Now.ToString("O");
            Timestamp = DateTimeOffset.Now;
        }
        
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string InitiatedBy { get; set; }
    }
}