using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.Shared.Storage.Azure;

public interface ITableStorage
{
    [UsedImplicitly]
    Task InsertOrMerge(string tableName, TableEntity tableEntity);
}
    
[UsedImplicitly]
public class TableStorage : ITableStorage
{
    private readonly string _azureStorageConnectionString;
        
    public TableStorage(string azureStorageConnectionString)
    {
        _azureStorageConnectionString = azureStorageConnectionString;
    }
        
    public async Task InsertOrMerge(string tableName, TableEntity tableEntity)
    {
        var cloudTable = await GetCloudTable(tableName);
            
        var insertOrMergeOperation = TableOperation.InsertOrMerge(tableEntity);
            
        await cloudTable.ExecuteAsync(insertOrMergeOperation);
    }

    private async Task<CloudTable> GetCloudTable(string tableName)
    {
        var storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
            
        var tableClient = storageAccount.CreateCloudTableClient();

        var tableReference =  tableClient.GetTableReference(tableName);

        await tableReference.CreateIfNotExistsAsync();

        return tableReference;
    }
}