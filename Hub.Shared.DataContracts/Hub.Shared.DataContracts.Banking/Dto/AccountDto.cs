using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class AccountDto : DtoBase
{
    [DataMember]
    public long? BankId { get; set; }
    
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public decimal Balance { get; set; }
    
    [DataMember]
    public bool BalanceIsAccumulated { get; set; }
    
    [DataMember]
    public bool NoBalanceForGivenPeriod { get; set; }
    
    [DataMember]
    public DateTime? BalanceDate { get; set; }

    [DataMember]
    public string AccountType { get; set; }
    
    [DataMember]
    public string AccountNumber { get; set; }
    
    [DataMember]
    public bool SharedAccount { get; set; }
    
    [DataMember] 
    public DateTime? DiscontinuedDate { get; set; }
    
    [DataMember]
    public BankDto Bank { get; set; }

}