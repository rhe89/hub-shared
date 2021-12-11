using System.Runtime.Serialization;

namespace Hub.Shared.DataContracts.Sbanken;

public class AccountDto : Hub.Shared.DataContracts.Banking.AccountDto
{
    [DataMember]
    public string AccountType { get; set; }
}