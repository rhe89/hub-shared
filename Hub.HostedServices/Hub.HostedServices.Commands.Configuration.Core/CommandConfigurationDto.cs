using System;
using Hub.Storage.Repository.Dto;

namespace Hub.HostedServices.Commands.Configuration.Core
{
    public class CommandConfigurationDto : EntityDtoBase
    {
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
        public string RunInterval { get; set; }
    }
}