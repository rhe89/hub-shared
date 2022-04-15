using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking;

[DataContract]
public class TransactionDto : EntityDtoBase
{
    [DataMember]
    public string Description { get; set; }

    [DataMember]
    public decimal Amount { get; set; }
        
    [DataMember]
    public long AccountId { get; set; }
        
    [DataMember]
    public DateTime TransactionDate { get; set; }
        
    [DataMember]
    public int TransactionType { get; set; }
        
    [DataMember]
    public string TransactionId { get; set; }
}