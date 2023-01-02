using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class AccountBalanceDto : DtoBase
{
    [DataMember]
    public long AccountId { get; set; }
    
    [DataMember]
    public decimal Balance { get; set; }
    
    [DataMember]
    public DateTime BalanceDate { get; set; }
    
    [DataMember]
    public AccountDto Account { get; set; }
}