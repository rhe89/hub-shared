using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class AssetHistoryDto : DtoBase
{
    [DataMember]
    public long AccountId { get; set; }

    [DataMember]
    public decimal Value { get; set; }
}