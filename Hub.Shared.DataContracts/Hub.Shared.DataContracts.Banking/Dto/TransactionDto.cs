using System;
using System.Runtime.Serialization;
using AutoMapper.Configuration.Annotations;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class TransactionDto : DtoBase
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
    public DateTime TransactionDate { get; set; }
        
    [DataMember]
    public int TransactionType { get; set; }
        
    [DataMember]
    public string TransactionId { get; set; }
    
    [DataMember]
    public bool Exclude { get; set; }
    
    [DataMember]
    public string Source { get; set; }
    
    [DataMember]
    [Ignore]
    public TransactionSubCategoryDto TransactionSubCategory { get; set; }
    
    [DataMember]
    [Ignore]
    public AccountDto Account { get; set; }
}