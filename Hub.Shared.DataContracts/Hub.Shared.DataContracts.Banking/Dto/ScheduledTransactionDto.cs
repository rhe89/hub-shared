using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using AutoMapper.Configuration.Annotations;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

public class ScheduledTransactionDto : DtoBase
{
    [DataMember]
    public long AccountId { get; set; }
    
    [DataMember] 
    public long? TransactionSubCategoryId { get; set; }
    
    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public decimal Amount { get; set; }
    
    [DataMember]
    public Guid TransactionKey { get; set; }

    [DataMember]
    public DateTime NextTransactionDate { get; set; }
    
    [DataMember]
    public Occurrence Occurrence { get; set; }
    
    [DataMember]
    public bool Completed { get; set; }
    
    [DataMember]
    [Ignore]
    public TransactionSubCategoryDto TransactionSubCategory { get; set; }
    
    [DataMember]
    public AccountDto Account { get; set; }
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

    [Display(Name = "Every six months")]
    Semiannually,
    
    [Display(Name = "Every year")]
    Annually,
    
    [Display(Name = "Every two years")]
    BiAnnually,
    
    [Display(Name = "Once")]
    Once
}