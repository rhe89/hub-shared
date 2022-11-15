using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class AccountBalanceDto : DtoBase
{
    [DataMember]
    public long AccountId { get; set; }

    [DataMember]
    public decimal Balance { get; set; }
    
    [UsedImplicitly]
    public DateTime BalanceDate { get; set; }
    
    [DataMember]
    public AccountDto Account { get; set; }
}