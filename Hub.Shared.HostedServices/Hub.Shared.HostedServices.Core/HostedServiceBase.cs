using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Hub.Shared.HostedServices.Commands;
using JetBrains.Annotations;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Hub.Shared.HostedServices.Core;

public abstract class HostedServiceBase : BackgroundService
{
    [UsedImplicitly]
    protected readonly ILogger<HostedServiceBase> Logger;
        
    [UsedImplicitly]
    protected readonly TelemetryClient TelemetryClient;

    private readonly IConfiguration _configuration;

    protected HostedServiceBase(ILogger<HostedServiceBase> logger, 
        IConfiguration configuration,
        TelemetryClient telemetryClient)
    {
        Logger = logger;
        _configuration = configuration;
        TelemetryClient = telemetryClient;
    }

    [UsedImplicitly]
    protected async Task ExecuteAsync(ICommand command,
        CancellationToken cancellationToken)
    {
        using var operation = TelemetryClient.StartOperation<DependencyTelemetry>($"Executing {command.Name}");
            
        await ExecuteAsync(command, operation, cancellationToken);
                
        operation.Telemetry.Stop();
    }
        
    [UsedImplicitly]
    protected async Task ExecuteAsync<TTelemetry>(ICommand command,
        IOperationHolder<TTelemetry> operation,
        CancellationToken cancellationToken) where TTelemetry : OperationTelemetry
    {
        try
        {
            Logger.LogInformation("Executing {CommandName}", command.Name);

            await command.Execute(cancellationToken);

            Logger.LogInformation("Executed {CommandName}", command.Name);

            SaveCommandLog(true, command.Name);

            if (command is ICommandWithConsumers commandWithConsumers)
            {
                await commandWithConsumers.NotifyConsumers();
            }

            operation.Telemetry.Success = true;
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Error occurred executing {CommandName}", command.Name);
                
            operation.Telemetry.Success = false;

            SaveCommandLog(false, command.Name, exception);
        }
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

            TelemetryClient.TrackEvent("CommandLog", new Dictionary<string, string>
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
            Logger.LogError(e, "Error occured when logging CommandLog for command with name {CommandName} in {BackgroundService}", commandName, GetType().Name);
        }
    }

}