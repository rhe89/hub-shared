namespace Hub.Shared.Storage.ServiceBus;

public static class QueueNames
{
    //Crypto
    public static readonly string UpdateCurrencyPrices = "updatecurrencyprices";

    //Banking
    public static readonly string UpdateScheduledTransactions = "updatescheduledtransactions";
    public static readonly string UpdateAccountBalancesForNewMonth = "updateaccountbalancesfornewmonth";
    public static readonly string UpdateSbankenTransactions = "updatesbankentransactions";
    public static readonly string UpdateBulderBankTransactions = "updatebulderbanktransactions";
    public static readonly string CalculateCreditCardPayments = "calculatecreditcardpayments";
    public static readonly string CalculateMonthlyBudget = "calculatemonthlybudgetresult";

    public static readonly string BankingAccountsUpdated = "bankingaccountsupdated";
    public static readonly string BankingTransactionsUpdated = "bankingtransactionsupdated";
}