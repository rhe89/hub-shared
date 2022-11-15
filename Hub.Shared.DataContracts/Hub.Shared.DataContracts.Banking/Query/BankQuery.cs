using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.Query;

[DataContract]
public class BankQuery : Storage.Repository.Core.Query
{
    [DataMember]
    [CanBeNull] 
    public string Name { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string AccountNumberPrefix { get; set; }
}