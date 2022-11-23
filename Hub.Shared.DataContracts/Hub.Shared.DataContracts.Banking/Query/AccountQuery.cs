using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.Query;

[DataContract]
public class AccountQuery : Storage.Repository.Core.Query
{ 
    [DataMember]
    [CanBeNull] 
    public long? AccountId { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public long[] AccountIds { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public long? BankId { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string AccountName { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string AccountType { get; set; }
    
    [DataMember]
    [CanBeNull] 
    public string AccountNumber { get; set; }
    
    [DataMember]
    public DateTime? BalanceFromDate { get; set; }
    
    [DataMember]
    public DateTime? BalanceToDate { get; set; }

    [DataMember]
    public DateTime? DiscontinuedDate { get; set; }
    
    [DataMember]
    public bool IncludeDiscontinuedAccounts { get; set; }
    
    [DataMember]
    public bool IncludeExternalAccounts { get; set; }

    [DataMember] 
    public bool IncludeSharedAccounts { get; set; }
    
}