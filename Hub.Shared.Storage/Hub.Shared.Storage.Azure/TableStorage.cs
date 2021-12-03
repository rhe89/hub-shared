using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.Shared.Storage.Azure
{
    public interface ITableStorage
    {
        Task InsertOrMerge(string tableName, TableEntity tableEntity);
    }
    
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
}