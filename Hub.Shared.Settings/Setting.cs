using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.Settings;

[DataContract]
public class Setting : EntityBase
{
    [DataMember]
    public string Key { get; set; }
        
    [DataMember]
    public string Value { get; set; }
}