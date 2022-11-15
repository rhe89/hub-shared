using System.Text.Json;

namespace Hub.Shared.Storage.ServiceBus.MessageBody;

public class BankingAccountBalancesUpdatedBody
{
    public int Month { get; set; }
    public int Year { get; set; }

    public static BankingAccountBalancesUpdatedBody Deserialize(string data)
    {
        return string.IsNullOrEmpty(data) ? 
            null :
            JsonSerializer.Deserialize<BankingAccountBalancesUpdatedBody>(data);
    }
}