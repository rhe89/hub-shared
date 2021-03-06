using System.Threading.Tasks;
using Hub.HostedServices.Commands.Logging.Core;
using Hub.Storage.Azure.Core;
using Microsoft.Extensions.Configuration;

namespace Hub.HostedServices.Commands.Logging
{
    public class CommandLogFactory : ICommandLogFactory
    {
        private readonly ITableStorage _tableStorage;
        private readonly string _logTableName;

        public CommandLogFactory(ITableStorage tableStorage,
            IConfiguration configuration)
        {
            _tableStorage = tableStorage;
            _logTableName = configuration.GetValue<string>("COMMAND_LOG_TABLE_NAME");
        }
        
        public async Task AddCommandLog(string commandName, bool success, string errorMessage, string initiatedBy, string domain)
        {
            var commandLog = new CommandLog(domain, commandName, success, initiatedBy, errorMessage);
            
            await _tableStorage.InsertOrMerge(_logTableName, commandLog);
        }
    }
}