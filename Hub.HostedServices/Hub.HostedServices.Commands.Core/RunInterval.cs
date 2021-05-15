using System.ComponentModel.DataAnnotations;

namespace Hub.HostedServices.Commands.Core
{
    public enum RunInterval
    {
        [Display(Name = "Hvert minutt")]
        Minute,
        [Display(Name = "Hver time")]
        Hour,
        [Display(Name = "Hver dag")]
        Day,
        [Display(Name = "Hver uke")]
        Week,
        [Display(Name = "Hver måned")]
        Month,
        [Display(Name = "Hvert år")]
        Year,
        [Display(Name = "Aldri")]
        Never
    }
}