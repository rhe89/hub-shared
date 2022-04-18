using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class AccountBalanceDto : EntityDtoBase
{
    [DataMember]
    public long AccountId { get; set; }

    [DataMember]
    public decimal Balance { get; set; }
}