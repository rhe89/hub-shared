using System;
using System.Threading;
using System.Threading.Tasks;
using Hub.HostedServices.Commands.Configuration.Core;
using Hub.HostedServices.Commands.Core;
using Microsoft.Extensions.Configuration;

namespace Hub.HostedServices.Schedule.Commands
{
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
        
        public DateTime LastRun => CommandConfiguration.LastRun;
        public string Name => GetType().FullName;
        public RunInterval RunInterval => Enum.Parse<RunInterval>(CommandConfiguration.RunInterval);
        
        public bool IsDue
        {
            get
            {
                return RunInterval switch
                {
                    RunInterval.Minute => LastRun < DateTime.Now.AddMinutes(-1),
                    RunInterval.Hour => LastRun < DateTime.Now.AddHours(-1),
                    RunInterval.Day => LastRun < DateTime.Now.AddDays(-1),
                    RunInterval.Week => LastRun < DateTime.Now.AddDays(-7),
                    RunInterval.Month => LastRun < DateTime.Now.AddMonths(-1),
                    RunInterval.Year => LastRun < DateTime.Now.AddYears(-1),
                    RunInterval.Never => false,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        } 
        
        public string NextScheduledRun
        {
            get { 
                return RunInterval switch
                {
                    RunInterval.Minute => LastRun.AddMinutes(1).ToString("f"),
                    RunInterval.Hour => LastRun.AddHours(1).ToString("f"),
                    RunInterval.Day => LastRun.AddDays(1).ToString("f"),
                    RunInterval.Week => LastRun.AddDays(7).ToString("f"),
                    RunInterval.Month => LastRun.AddMonths(1).ToString("f"),
                    RunInterval.Year => LastRun.AddYears(1).ToString("f"),
                    RunInterval.Never => "Never",
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        private CommandConfigurationDto CommandConfiguration
        {
            get
            {
                var commandConfiguration = _commandConfigurationProvider.Get(Name) ??
                                            _commandConfigurationFactory.CreateDefaultCommandConfiguration(Name);
                
                return commandConfiguration;
            }
        }
    }
}