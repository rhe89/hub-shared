using System;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.HostedServices.Commands
{
    public class CommandConfiguration : EntityBase
    {
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
        public string RunInterval { get; set; }
    }
}