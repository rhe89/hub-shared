using System;
using System.Threading;
using System.Threading.Tasks;
using Hub.HostedServices.Commands.Core;
using Hub.HostedServices.Commands.Logging.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.HostedServices.Core
{
    public abstract class HostedServiceBase : BackgroundService
    {
        protected readonly ILogger<HostedServiceBase> Logger;
        private readonly ICommandLogFactory _commandLogFactory;
        private readonly IConfiguration _configuration;

        protected HostedServiceBase(ILogger<HostedServiceBase> logger, 
            ICommandLogFactory commandLogFactory, 
            IConfiguration configuration)
        {
            Logger = logger;
            _commandLogFactory = commandLogFactory;
            _configuration = configuration;
        }

        protected async Task ExecuteAsync(ICommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"{command.Name} starting.");

                await command.Execute(cancellationToken);

                await SaveCommandLog(true, command.Name);

                if (command is ICommandWithConsumers commandWithConsumers)
                {
                    await commandWithConsumers.NotifyConsumers();
                }

            }
            catch (Exception exception)
            {
                Logger.LogError(exception, $"Error occurred executing {command.Name}.");
                await SaveCommandLog(false, command.Name, exception);
            }
            
            Logger.LogInformation($"{command.Name} finished.");
        }
            
        private async Task SaveCommandLog(bool success, string commandName, Exception exception = null)
        {
            try
            {
                var errorMessage = exception == null
                        ? ""
                        : $"Exception: {exception.Message}. Inner exception: {exception.InnerException}. Stacktrace: {exception.StackTrace}";

                var initiatedBy = GetType().FullName;

                var domain = _configuration.GetValue<string>("DOMAIN");

                await _commandLogFactory.AddCommandLog(commandName, success, errorMessage, initiatedBy, domain);
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error occured when saving CommandLog for command with name {commandName} in {GetType().Name}");
            }
        }

    }
}
