using System;
using System.Threading;
using System.Threading.Tasks;
using Hub.HostedServices.Core.Tasks;
using Hub.Storage.Core.Dto;
using Hub.Storage.Core.Factories;
using Hub.Storage.Core.Providers;

namespace Hub.HostedServices.Tasks
{
    public abstract class BackgroundTask : IBackgroundTask
        {
            private readonly IBackgroundTaskConfigurationProvider _backgroundTaskConfigurationProvider;
            private readonly IBackgroundTaskConfigurationFactory _backgroundTaskConfigurationFactory;

            public BackgroundTask(IBackgroundTaskConfigurationProvider backgroundTaskConfigurationProvider,
                IBackgroundTaskConfigurationFactory backgroundTaskConfigurationFactory)
            {
                _backgroundTaskConfigurationProvider = backgroundTaskConfigurationProvider;
                _backgroundTaskConfigurationFactory = backgroundTaskConfigurationFactory;
            }

            public async Task UpdateLastRun(DateTime lastRun)
            {
                await _backgroundTaskConfigurationFactory.UpdateLastRun(Name, lastRun);
            }

            public DateTime LastRun => BackgroundTaskConfiguration.LastRun;
            public RunIntervalType RunIntervalType => BackgroundTaskConfiguration.RunIntervalType;
            public string Name => GetType().Name;
            
            public bool IsDue
            {
                get
                {
                    return RunIntervalType switch
                    {
                        RunIntervalType.Minute => LastRun < DateTime.Now.AddMinutes(-1),
                        RunIntervalType.Hour => LastRun < DateTime.Now.AddHours(-1),
                        RunIntervalType.Day => LastRun < DateTime.Now.AddDays(-1),
                        RunIntervalType.Week => LastRun < DateTime.Now.AddDays(-7),
                        RunIntervalType.Month => LastRun < DateTime.Now.AddMonths(-1),
                        RunIntervalType.Year => LastRun < DateTime.Now.AddYears(-1),
                        RunIntervalType.Never => false,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            } 
                
            
            public string NextScheduledRun
            {
                get { 
                    return RunIntervalType switch
                    {
                        RunIntervalType.Minute => LastRun.AddMinutes(1).ToString("f"),
                        RunIntervalType.Hour => LastRun.AddHours(1).ToString("f"),
                        RunIntervalType.Day => LastRun.AddDays(1).ToString("f"),
                        RunIntervalType.Week => LastRun.AddDays(7).ToString("f"),
                        RunIntervalType.Month => LastRun.AddMonths(1).ToString("f"),
                        RunIntervalType.Year => LastRun.AddYears(1).ToString("f"),
                        RunIntervalType.Never => "Never",
                        _ => throw new ArgumentOutOfRangeException()
                    };
                }
            }

            public bool ManualExecution { get; set; }

            public abstract Task Execute(CancellationToken cancellationToken);
            
            private BackgroundTaskConfigurationDto BackgroundTaskConfiguration
            {
                get
                {
                    var backgroundTaskConfiguration = _backgroundTaskConfigurationProvider.Get(Name) ??
                                                      _backgroundTaskConfigurationFactory.CreateDefaultBackgroundTaskConfiguration(Name);

                    return backgroundTaskConfiguration;
                }
            }


        }
}