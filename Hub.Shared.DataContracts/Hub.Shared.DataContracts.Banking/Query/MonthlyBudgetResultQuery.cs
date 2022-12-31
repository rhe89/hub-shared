using System;
using System.Runtime.Serialization;

namespace Hub.Shared.DataContracts.Banking.Query;

[DataContract]
public class MonthlyBudgetQuery : Storage.Repository.Core.Query
{
    [DataMember]
    public DateTime? Month { get; set; }
}