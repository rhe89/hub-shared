using System;

namespace Hub.Shared.DataContracts.Banking.SearchParameters;

public class AccountBalanceSearchParameters : AccountSearchParameters
{
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}