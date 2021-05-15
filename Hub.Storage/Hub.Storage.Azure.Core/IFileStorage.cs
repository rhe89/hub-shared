using System.Threading.Tasks;

namespace Hub.Storage.Azure.Core
{
    public interface IFileStorage
    {
        Task<byte[]> GetItem(string fileShare, string folder, string fileReference);
    }
}