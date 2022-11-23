using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.Query;

[DataContract]
public class TransactionCategoryQuery : Storage.Repository.Core.Query
{
    [DataMember]
    [CanBeNull]
    public string Name { get; set; }
}