using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class CurrencyDto : DtoBase
{
    [DataMember]
    public string Name { get; set; }
    
    [DataMember]
    public decimal Price { get; set; }
    
    [DataMember]
    public DateTime? PriceDate { get; set; }
}