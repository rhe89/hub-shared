using System;
using System.Runtime.Serialization;
using Hub.Shared.Storage.Repository.Core;

namespace Hub.Shared.DataContracts.Banking.Dto;

[DataContract]
public class MonthlyBudgetDto : DtoBase
{
    [DataMember] 
    public DateTime Month { get; set; }

    [DataMember]
    public decimal Income { get; set; }

    [DataMember]
    public decimal Savings { get; set; }
    
    [DataMember]
    public decimal Mortgage { get; set; }
    
    [DataMember]
    public decimal SharedAccountTransactions { get; set; }

    [DataMember]
    public decimal Investments { get; set; }

    [DataMember]
    public decimal Bills { get; set; }
    
    [DataMember]
    public decimal Result { get; set; }

}