using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class WalletDto : DtoBase
{
    [DataMember]
    public string Name { get; set; }
}