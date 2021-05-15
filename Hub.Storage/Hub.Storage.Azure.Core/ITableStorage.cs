using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Hub.Storage.Azure.Core
{
    public interface ITableStorage
    {
        Task InsertOrMerge(string tableName, TableEntity tableEntity);
    }
}