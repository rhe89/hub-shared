using System;
using System.Runtime.Serialization;

namespace Hub.Shared.DataContracts.Crypto.Query;

[DataContract]
public class AssetHistoryQuery : AccountQuery
{
    [DataMember]
    public DateTime? FromDate { get; set; }
    
    [DataMember]
    public DateTime? ToDate { get; set; }
}