namespace Hub.Shared.Storage.ServiceBus;

public static class QueueNames
{
    //Crypto
    public static readonly string UpdateCoinbaseProAccounts = "updatecoinbaseproaccounts";
    public static readonly string UpdateCoinbaseProAssetHistory = "updatecoinbaseproassethistory";

    public static readonly string UpdateCoinbaseAccounts = "updatecoinbaseaccounts";
    public static readonly string UpdateCoinbaseExchangeRates = "updatecoinbaseexchangerates";
    public static readonly string UpdateCoinbaseAssetHistory = "updatecoinbaseassethistory";

    public static readonly string CryptoAssetHistoryUpdated = "cryptoassethistoryupdated";
    public static readonly string CryptoAccountsUpdated = "cryptoaccountsupdated";
    public static readonly string ExchangeRatesUpdated = "exchangeratesupdated";

    //Banking
    public static readonly string UpdateScheduledTransactions = "updatescheduledtransactions";
    public static readonly string UpdateAccountBalancesForNewMonth = "updateaccountbalancesfornewmonth";
    public static readonly string UpdateSbankenTransactions = "updatesbankentransactions";
    public static readonly string UpdateBulderBankTransactions = "updatebulderbanktransactions";
    public static readonly string CategorizeTransactions = "categorizetransactions";
    public static readonly string CalculateCreditCardPayments = "calculatecreditcardpayments";
    public static readonly string CalculateMonthlyBudget = "calculatemonthlybudgetresult";

    public static readonly string BankingAccountsUpdated = "bankingaccountsupdated";
    public static readonly string BankingTransactionsUpdated = "bankingtransactionsupdated";
}