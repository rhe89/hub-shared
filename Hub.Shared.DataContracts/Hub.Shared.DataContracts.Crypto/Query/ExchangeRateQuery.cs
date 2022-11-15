using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Crypto.Query;

[DataContract]
public class ExchangeRateQuery : Storage.Repository.Core.Query
{
    [DataMember]
    [CanBeNull]
    public string Currency { get; init; }
}