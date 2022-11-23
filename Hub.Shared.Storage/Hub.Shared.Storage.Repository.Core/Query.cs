using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.Storage.Repository.Core;

[DataContract]
public abstract class Query
{
    [CanBeNull]
    [DataMember]
    public long? Id { get; set; }
    
    [CanBeNull]
    [DataMember]
    public int? Take { get; set; }

    [DataMember]
    public int? Skip { get; set; }
}