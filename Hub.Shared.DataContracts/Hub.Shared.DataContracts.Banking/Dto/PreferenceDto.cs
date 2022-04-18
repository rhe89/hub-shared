using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class PreferenceDto : EntityDtoBase
{
    [DataMember]
    public string Key { get; set; }
    
    [DataMember]
    public string Value { get; set; }
}