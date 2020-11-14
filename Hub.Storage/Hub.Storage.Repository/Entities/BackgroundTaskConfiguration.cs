using System;
using Hub.Storage.Core;

namespace Hub.Storage.Repository.Entities
{
    public class BackgroundTaskConfiguration : EntityBase
    {
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
        public int RunIntervalType { get; set; }
    }
}