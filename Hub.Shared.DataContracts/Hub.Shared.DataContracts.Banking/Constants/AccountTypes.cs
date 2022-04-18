namespace Hub.Shared.DataContracts.Banking.Constants;

public static class AccountTypes
{
    public static readonly string Standard = "Standard account";
    public static readonly string Billing = "Billing account";
    public static readonly string Saving = "Savings account";
    public static readonly string CreditCard = "Credit card account";

    public static string[] ToArray => new[] { Standard, Billing, Saving, CreditCard };
}