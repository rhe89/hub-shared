using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class BankDto : DtoBase
{
    [DataMember]
    public string Name { get; set; }
    
    [DataMember]
    public string AccountNumberPrefix { get; set; }

}