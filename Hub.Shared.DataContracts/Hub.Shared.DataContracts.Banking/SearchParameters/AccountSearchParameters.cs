using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.SearchParameters;

public class AccountSearchParameters
{ 
    [CanBeNull] public string[] Banks { get; set; }
    [CanBeNull] public string[] AccountNames { get; set; }
    [CanBeNull] public string[] AccountTypes { get; set; }
    [CanBeNull] public long[] AccountIds { get; set; }
    public bool MergeAccountsWithSameNameFromDifferentBanks { get; set; }
    public int? Take { get; set; }
}