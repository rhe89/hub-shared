using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto;

[DataContract]
public class AccountBalanceDto : EntityDtoBase
{
    [DataMember]
    public long AccountId { get; set; }

    [DataMember]
    public decimal Balance { get; set; }
}