using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking
{
    [DataContract]
    public class AccountDto : EntityDtoBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Balance { get; set; }
    }
}
