using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Crypto.Dto;

[DataContract]
public class ExchangeRateDto : EntityDtoBase
{
    [DataMember]
    public string Currency { get; set; }
        
    [DataMember]
    public decimal NOKRate { get; set; }
        
    [DataMember]
    public decimal USDRate { get; set; }
        
    [DataMember]
    public decimal EURRate { get; set; }
}