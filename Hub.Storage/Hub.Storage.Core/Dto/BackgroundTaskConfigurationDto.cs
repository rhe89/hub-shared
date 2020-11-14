using System;
using System.ComponentModel.DataAnnotations;

namespace Hub.Storage.Core.Dto
{
    public class BackgroundTaskConfigurationDto
    {
        public string Name { get; set; }
        public DateTime LastRun { get; set; }
        public RunIntervalType RunIntervalType { get; set; }
    }
    
    public enum RunIntervalType
    {
        [Display(Name = "Hvert minutt")]
        Minute = 1,
        [Display(Name = "Hver time")]
        Hour = 2,
        [Display(Name = "Hver dag")]
        Day = 3,
        [Display(Name = "Hver uke")]
        Week = 4,
        [Display(Name = "Hver måned")]
        Month = 5,
        [Display(Name = "Hvert år")]
        Year = 6,
        [Display(Name = "Aldri")]
        Never = 7
    }
}