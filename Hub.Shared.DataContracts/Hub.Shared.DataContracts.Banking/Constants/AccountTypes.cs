namespace Hub.Shared.DataContracts.Banking.Constants;

public static class AccountTypes
{
    public const string Standard = "Standard account";
    public const string Billing = "Billing account";
    public const string Saving = "Savings account";
    public const string CreditCard = "Credit card account";
    public const string Investment = "Investment account";

    public static string[] ToArray => new[] { Standard, Billing, Saving, Investment, CreditCard };
}