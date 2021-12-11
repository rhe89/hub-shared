using System;
using System.Runtime.Serialization;

namespace Hub.Shared.Storage.Repository.Core;

[DataContract]
public class EntityDtoBase
{
    [DataMember]
    public long Id { get; set; }

    [DataMember]
    public DateTime CreatedDate { get; set; }
        
    [DataMember]
    public DateTime UpdatedDate { get; set; }
}