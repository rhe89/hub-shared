using System;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.HostedServices.Commands
{
    public class CommandConfigurationDto : EntityDtoBase
    {
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
        public string RunInterval { get; set; }
    }
}