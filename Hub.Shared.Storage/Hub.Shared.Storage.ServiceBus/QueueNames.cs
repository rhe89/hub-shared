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
    public static readonly string UpdateScheduledTransactions = "updaterecurringtransactions";
    public static readonly string UpdateAccountBalancesForNewMonth = "updateaccountbalancesfornewmonth";
    public static readonly string UpdateSbankenTransactions = "updatesbankentransactions";
    public static readonly string ImportTransactionsCsv = "importtransactionscsv";
    public static readonly string CategorizeTransactions = "categorizetransactions";
    public static readonly string CalculateCreditCardPayments = "calculatecreditcardpayments";
    public static readonly string ScheduledTransactionsUpdated = "recurringtransactionsupdated";

    public static readonly string BankingAccountsUpdated = "bankingaccountsupdated";
    public static readonly string BankingTransactionsUpdated = "bankingtransactionsupdated";
    public static readonly string BankingAccountBalanceHistoryUpdated = "bankingaccountbalancehistoryupdated";
}