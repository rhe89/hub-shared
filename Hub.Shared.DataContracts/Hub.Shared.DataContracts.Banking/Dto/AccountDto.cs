using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class AccountDto : EntityDtoBase
{
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public decimal Balance { get; set; }
    
    [DataMember]
    public string AccountType { get; set; }
    
    [DataMember]
    public string Bank { get; set; }
}