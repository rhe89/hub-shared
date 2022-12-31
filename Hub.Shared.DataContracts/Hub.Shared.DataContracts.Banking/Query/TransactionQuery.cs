using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.Query;

[DataContract]
public class TransactionQuery : Storage.Repository.Core.Query
{
    [DataMember]
    [CanBeNull]
    public long? BankId { get; set; }
    
    [DataMember]
    [CanBeNull]
    public long? AccountId { get; set; }
    
    [DataMember]
    [CanBeNull]
    public long[] AccountIds { get; set; }
    
    [DataMember]
    [CanBeNull]
    public long? TransactionCategoryId { get; set; }
    
    [DataMember] 
    [CanBeNull]
    public long? TransactionSubCategoryId { get; set; }
    
    [DataMember]
    public DateTime? FromDate { get; set; }
    
    [DataMember]
    public DateTime? ToDate { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string Description { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string AccountName { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string AccountType { get; set; }
    
    [DataMember]
    public bool IncludeTransactionsFromSharedAccounts { get; set; }

    [DataMember]
    public bool IncludeExcludedTransactions { get; set; }
    
    [DataMember]
    [CanBeNull]
    public string Source { get; set; }
    
}