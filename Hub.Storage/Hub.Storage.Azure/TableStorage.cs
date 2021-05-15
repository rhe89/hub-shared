using System.Threading.Tasks;
using Hub.Storage.Azure.Core;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.Storage.Azure
{
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