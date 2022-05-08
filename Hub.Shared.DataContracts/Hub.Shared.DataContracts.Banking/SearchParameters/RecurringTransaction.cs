using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.SearchParameters;

public class RecurringTransactionSearchParameters
{
    public long? RecurringTransactionId { get; set; }
    [CanBeNull] public long[] AccountIds { get; set; }
    [CanBeNull] public string Description { get; set; }
    public int? Take { get; set; }
}