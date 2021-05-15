using System;
using Hub.Storage.Repository.Entities;

namespace Hub.HostedServices.Commands.Configuration.Core
{
    public class CommandConfiguration : EntityBase
    {
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
        public string RunInterval { get; set; }
    }
}