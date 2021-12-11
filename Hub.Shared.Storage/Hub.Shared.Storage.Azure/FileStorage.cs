using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.File;

namespace Hub.Shared.Storage.Azure;

public interface IFileStorage
{
    [UsedImplicitly]
    Task<byte[]> GetItem(string fileShare, string folder, string fileReference);
}
    
[UsedImplicitly]
public class FileStorage : IFileStorage
{
    private readonly string _azureStorageConnectionString;

    public FileStorage(string azureStorageConnectionString)
    {
        _azureStorageConnectionString = azureStorageConnectionString;
    }
        
    public async Task<byte[]> GetItem(string fileShare, string folder, string fileReference)
    {
        var cloudFileDirectory = GetCloudFileDirectory(fileShare, folder);
            
        var file = cloudFileDirectory.GetFileReference(fileReference);
        var byteArr = new byte[file.StreamMinimumReadSizeInBytes];

        await file.DownloadToByteArrayAsync(byteArr, 0);

        return byteArr;
    }
        
    private CloudFileDirectory GetCloudFileDirectory(string fileShare, string folder)
    {
        var storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);

        var fileClient = storageAccount.CreateCloudFileClient();

        var share = fileClient.GetShareReference(fileShare);

        var rootDir = share.GetRootDirectoryReference();

        var cloudFileDirectory = rootDir.GetDirectoryReference(folder);

        return cloudFileDirectory;
    }

}