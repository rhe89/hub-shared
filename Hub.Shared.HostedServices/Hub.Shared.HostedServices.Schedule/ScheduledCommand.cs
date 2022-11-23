using System;
using System.Threading;
using System.Threading.Tasks;
using Hub.Shared.HostedServices.Commands;
using JetBrains.Annotations;

namespace Hub.Shared.HostedServices.Schedule;

public interface IScheduledCommand : ICommand
{
    [UsedImplicitly]
    bool IsDue { get; }
        
    [UsedImplicitly]
    string NextScheduledRun { get; }
        
    [UsedImplicitly]
    Task UpdateLastRun(DateTime lastRun);
}
    
[UsedImplicitly]
public abstract class ScheduledCommand : IScheduledCommand
{
    private readonly ICommandConfigurationProvider _commandConfigurationProvider;
    private readonly ICommandConfigurationFactory _commandConfigurationFactory;

    protected ScheduledCommand(ICommandConfigurationProvider commandConfigurationProvider,
        ICommandConfigurationFactory commandConfigurationFactory)
    {
        _commandConfigurationProvider = commandConfigurationProvider;
        _commandConfigurationFactory = commandConfigurationFactory;
    }

    public async Task UpdateLastRun(DateTime lastRun)
    {
        await _commandConfigurationFactory.UpdateLastRun(Name, lastRun);
    }
        
    public abstract Task Execute(CancellationToken cancellationToken);
    public string Name => GetType().Name;
        
    private RunInterval RunInterval => Enum.Parse<RunInterval>(CommandConfiguration.RunInterval);
    private DateTime LastRun => CommandConfiguration.LastRun;

    public bool IsDue
    {
        get
        {
            return RunInterval switch
            {
                RunInterval.Minute => LastRun < DateTime.Now.AddMinutes(-1),
                RunInterval.Hour => LastRun < DateTime.Now.AddHours(-1),
                RunInterval.Day => LastRun.Date < DateTime.Now.Date.AddDays(-1),
                RunInterval.Week => LastRun.Date < DateTime.Now.Date.AddDays(-7),
                RunInterval.Month => new DateTime(LastRun.Date.Year, LastRun.Date.Month, 1).Date < new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                RunInterval.Year => new DateTime(LastRun.Date.Year, LastRun.Date.Month, 1).Date < new DateTime(DateTime.Now.Year, 1, 1),
                RunInterval.Never => false,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    } 
        
    public string NextScheduledRun
    {
        get
        {
            return RunInterval switch
            {
                RunInterval.Minute => LastRun.AddMinutes(1).ToString("f"),
                RunInterval.Hour => LastRun.AddHours(1).ToString("f"),
                RunInterval.Day => LastRun.Date.AddDays(1).ToString("f"),
                RunInterval.Week => LastRun.Date.AddDays(7).ToString("f"),
                RunInterval.Month => new DateTime(LastRun.Date.AddMonths(1).Year, LastRun.Date.AddMonths(1).Month, 1).ToString("f"),
                RunInterval.Year => new DateTime(LastRun.Date.AddYears(1).Year, LastRun.Date.AddYears(1).Month, 1).ToString("f"),
                RunInterval.Never => "Never",
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
        
    private CommandConfigurationDto CommandConfiguration
    {
        get
        {
            var commandConfiguration = _commandConfigurationProvider.Get(Name).GetAwaiter().GetResult() ??
                                       _commandConfigurationFactory.CreateDefaultCommandConfiguration(Name);
                
            return commandConfiguration;
        }
    }
}