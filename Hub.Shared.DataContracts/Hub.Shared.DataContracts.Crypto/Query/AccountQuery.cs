using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Crypto.Query;

[DataContract]
public class AccountQuery : Storage.Repository.Core.Query
{ 
    [DataMember]
    [CanBeNull] 
    public string Exchange { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string Currency { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public long? AccountId { get; set; }
    
    [DataMember]
    public bool MergeAccountsWithSameNameFromDifferentExchanges { get; init; }
}