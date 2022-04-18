using System;

namespace Hub.Shared.DataContracts.Crypto.SearchParameters;

public class AssetHistorySearchParameters : AccountSearchParameters
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}