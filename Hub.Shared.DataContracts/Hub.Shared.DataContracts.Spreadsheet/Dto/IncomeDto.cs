using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Hub.Shared.DataContracts.Spreadsheet.Dto;

[DataContract]
public class IncomeDto
{
    [DataMember]
    public DateTime Month { get; set; }
    
    [DataMember]
    public decimal Amount { get; set; }
    
}