using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class CurrencyPriceDto : DtoBase
{
    [DataMember]
    public long CurrencyId { get; set; }
    
    [DataMember]
    public decimal Price { get; set; }
    
    [DataMember]
    public DateTime PriceDate { get; set; }

    [DataMember]
    public CurrencyDto CurrencyDto { get; set; }
}