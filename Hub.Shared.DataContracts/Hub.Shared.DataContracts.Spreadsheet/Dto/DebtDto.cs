using System;

namespace Hub.Shared.DataContracts.Spreadsheet.Dto;

public class DebtDto
{
    public DateTime Month { get; set; }
    public decimal Mortgage { get; set; }
    public decimal StudentLoan { get; set; }
}