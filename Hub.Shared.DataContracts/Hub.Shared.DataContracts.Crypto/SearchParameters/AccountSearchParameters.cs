using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Crypto.SearchParameters;

public class AccountSearchParameters
{ 
    [CanBeNull] public string[] Exchanges { get; set; }
    [CanBeNull] public string[] Currencies { get; set; }
    [CanBeNull] public long[] AccountIds { get; set; }
    public bool MergeAccountsWithSameNameFromDifferentExchanges { get; set; }
}