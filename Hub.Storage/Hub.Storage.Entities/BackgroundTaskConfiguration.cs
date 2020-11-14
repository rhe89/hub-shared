using System;

namespace Hub.Storage.Entities
{
    public class BackgroundTaskConfiguration : EntityBase
    {
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
        public int RunIntervalType { get; set; }
    }
}