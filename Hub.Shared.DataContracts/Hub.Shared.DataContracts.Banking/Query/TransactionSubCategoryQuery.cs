using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.Query;

[DataContract]
public class TransactionSubCategoryQuery : Storage.Repository.Core.Query
{
    [DataMember]
    [CanBeNull]
    public long? TransactionCategoryId { get; set; }
    
    [DataMember]
    [CanBeNull]
    public long[] TransactionCategoryIds { get; set; }
    
    [DataMember]
    [CanBeNull]
    public string TransactionCategoryName { get; set; }
    
    [DataMember]
    [CanBeNull]
    public string Name { get; set; }
}