using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Crypto.Query;

[DataContract]
public class CurrencyQuery : Storage.Repository.Core.Query
{
    [DataMember]
    [CanBeNull]
    public long? CurrencyId { get; set; }
    
    [DataMember]
    [CanBeNull]
    public string Name { get; set; }
    
    [DataMember]
    [CanBeNull]
    public DateTime? PriceFromDate { get; set; }
    
    [DataMember]
    [CanBeNull]
    public DateTime? PriceToDate { get; set; }
}