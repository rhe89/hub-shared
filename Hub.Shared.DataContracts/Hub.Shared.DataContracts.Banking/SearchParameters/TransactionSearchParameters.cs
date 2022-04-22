using System;
using JetBrains.Annotations;

namespace Hub.Shared.DataContracts.Banking.SearchParameters;

public class TransactionSearchParameters
{
        public long? TransactionId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        [CanBeNull] public string Description { get; set; }
        [CanBeNull] public string[] AccountNames { get; set; }
        [CanBeNull] public string[] AccountTypes { get; set; }
        [CanBeNull] public int[] Months { get; set; }
        [CanBeNull] public int[] Years { get; set; }
        [CanBeNull] public long[] AccountIds { get; set; }
        public int? Take { get; set; }
}