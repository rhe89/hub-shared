using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class AccountDto : DtoBase
{
    [DataMember]
    public string Name { get; set; }
    
    [DataMember]
    public long WalletId { get; set; }
    
    [DataMember]
    public long CurrencyId { get; set; }

    [DataMember]
    public WalletDto Wallet { get; set; }
    
    [DataMember]
    public CurrencyDto Currency { get; set; }
    
    [DataMember] 
    public decimal Balance { get; set; }
    
    [DataMember] 
    public DateTime? BalanceDate { get; set; }

    [DataMember]
    public bool NoBalanceForGivenPeriod { get; set; }
}