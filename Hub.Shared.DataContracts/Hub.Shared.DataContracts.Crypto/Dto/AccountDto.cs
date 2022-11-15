using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class AccountDto : DtoBase
{
    [DataMember]
    public string Currency { get; set; }

    [DataMember]
    public decimal Balance { get; set; }
    
    [DataMember]
    public string Exchange { get; set; }
    
    [DataMember] 
    public bool MergedAccount { get; set; }
}