using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.Query;

[DataContract]
public class ScheduledTransactionQuery : Storage.Repository.Core.Query
{
    [DataMember] 
    [CanBeNull]
    public long? AccountId { get; set; }
    
    [DataMember] 
    [CanBeNull]
    public decimal[] AmountRange { get; set; }
    
    [DataMember]
    public Guid? TransactionKey { get; set; }

    [DataMember] 
    public long? TransactionSubCategoryId { get; set; }
    
    [DataMember]
    public DateTime? NextTransactionFromDate { get; set; }
    
    [DataMember]
    public DateTime? NextTransactionToDate { get; set; }

    [DataMember]
    [CanBeNull] 
    public string Description { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string AccountType { get; set; }
    
    [DataMember]
    public bool IncludeCompletedTransactions { get; set; }
}