using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Crypto.Query;

[DataContract]
public class WalletQuery : Storage.Repository.Core.Query
{
    [DataMember]
    [CanBeNull]
    public string Name { get; set; }
}