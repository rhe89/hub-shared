using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hub.Shared.HostedServices.Commands;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.HostedServices.Core
{
    public abstract class HostedServiceBase : BackgroundService
    {
        protected readonly ILogger<HostedServiceBase> Logger;
        private readonly IConfiguration _configuration;
        private readonly TelemetryClient _telemetryClient;

        protected HostedServiceBase(ILogger<HostedServiceBase> logger, 
            IConfiguration configuration,
            TelemetryClient telemetryClient)
        {
            Logger = logger;
            _configuration = configuration;
            _telemetryClient = telemetryClient;
        }

        protected async Task ExecuteAsync(ICommand command,
            CancellationToken cancellationToken)
        {
            try
            {
                Logger.LogInformation($"{command.Name} starting.");
                _telemetryClient.TrackEvent($"{command.Name} starting.");

                await command.Execute(cancellationToken);

                SaveCommandLog(true, command.Name);

                if (command is ICommandWithConsumers commandWithConsumers)
                {
                    await commandWithConsumers.NotifyConsumers();
                }

            }
            catch (Exception exception)
            {
                Logger.LogError(exception, $"Error occurred executing {command.Name}.");
                _telemetryClient.TrackException(exception);

                SaveCommandLog(false, command.Name, exception);
            }
            
            Logger.LogInformation($"{command.Name} finished.");
            _telemetryClient.TrackEvent($"{command.Name} finished.");
        }
            
        private void SaveCommandLog(bool success, string commandName, Exception exception = null)
        {
            try
            {
                var errorMessage = exception == null
                        ? ""
                        : $"Exception: {exception.Message}. Inner exception: {exception.InnerException}. Stacktrace: {exception.StackTrace}";

                var initiatedBy = GetType().Name;

                var domain = _configuration.GetValue<string>("DOMAIN");

                _telemetryClient.TrackEvent("CommandLog", new Dictionary<string, string>
                {
                    {"CommandName", commandName},
                    {"InitiatedBy", initiatedBy},
                    {"Domain", domain},
                    {"Success", success.ToString()},
                    {"ErrorMessage", errorMessage}
                });
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error occured when saving CommandLog for command with name {commandName} in {GetType().Name}");
            }
        }

    }
}
