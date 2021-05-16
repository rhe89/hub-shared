using System;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.HostedServices.Commands.Logging
{
    public class CommandLog : TableEntity
    {
        public CommandLog()
        {
            
        }

        public CommandLog(string domain, string commandName, bool success, string initiatedBy, string errorMessage)
        {
            var now = DateTime.Now;
            
            PartitionKey = now.ToString("yyyy-MM-dd");
            RowKey = now.ToString("hh:mm:ss.ffff");
            Timestamp = DateTimeOffset.Now;
            
            Domain = domain;
            CommandName = commandName;
            Success = success;
            InitiatedBy = initiatedBy;
            ErrorMessage = errorMessage;
        }

        public string Domain { get; set; }
        public string CommandName { get; set; }
        public bool Success { get; set; }
        public string InitiatedBy { get; set; }
        public string ErrorMessage { get; set; }
    }
}