using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

public class RecurringTransactionDto : EntityDtoBase
{
    [DataMember]
    public string Description { get; set; }
        
    [DataMember]
    public decimal Amount { get; set; }
    
    [DataMember]
    public long AccountId { get; set; }
    
    [DataMember]
    public DateTime LatestTransactionCreated { get; set; }
    
    [DataMember]
    public DateTime NextTransactionDate { get; set; }
    
    [DataMember]
    public Occurrence Occurrence { get; set; }
}

public enum Occurrence
{
    [Display(Name = "Every day")]
    Daily,
    
    [Display(Name = "Every week")]
    Weekly,
    
    [Display(Name = "Every two weeks")]
    BiWeekly,
    
    [Display(Name = "Every month")]
    Monthly,
    
    [Display(Name = "Every two months")]
    BiMonthly,
    
    [Display(Name = "Every three months (quarterly)")]
    Quarterly,

    [Display(Name = "Twice a year")]
    Semiannually,
    
    [Display(Name = "Every year")]
    Annually,
    
    [Display(Name = "Every two years")]
    BiAnnually
}