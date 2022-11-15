using System.Text.Json;

namespace Hub.Shared.Storage.ServiceBus.MessageBody;

public class BankingTransactionsUpdatedBody
{
    public int Month { get; set; }
    public int Year { get; set; }

    public static BankingTransactionsUpdatedBody Deserialize(string data)
    {
        return string.IsNullOrEmpty(data) ? 
            null :
            JsonSerializer.Deserialize<BankingTransactionsUpdatedBody>(data);
    }
}