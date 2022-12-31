using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;

namespace Hub.Shared.GoogleApi;

public static class GoogleDriveService
{
    public static Task<FileList> ListFiles(string folderName, IConfiguration configuration)
    {
        var credential = CredentialProvider.GetServiceAccountCredential(configuration, new[] { DriveService.Scope.Drive });

        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Hub"
        });

        var listRequest = service.Files.List();

        listRequest.PageSize = 1000;
        listRequest.Q = $"'{folderName}' in parents";

        return listRequest.ExecuteAsync();
    }
    
    public static async Task DeleteFile(string fileId, IConfiguration configuration)
    {
        var credential = CredentialProvider.GetServiceAccountCredential(configuration, new[] { DriveService.Scope.Drive });

        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Hub"
        });

        await service.Files.Delete(fileId).ExecuteAsync();
    }
    
    public static async Task<MemoryStream> DownloadFile(string fileId, IConfiguration configuration)
    {
        var credential = CredentialProvider.GetServiceAccountCredential(configuration, new[] { DriveService.Scope.Drive });

        var service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = "Hub"
        });

        var request = service.Files.Get(fileId);

        request.MediaDownloader.ProgressChanged +=
            progress =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                    {
                        Console.WriteLine(progress.BytesDownloaded);
                        break;
                    }
                    case DownloadStatus.Completed:
                    {
                        Console.WriteLine("Download complete.");
                        break;
                    }
                    case DownloadStatus.Failed:
                    {
                        Console.WriteLine("Download failed.");
                        break;
                    }
                }
            };
        
        var stream = new MemoryStream();

        await request.DownloadAsync(stream);
        
        stream.Position = 0;
        return stream;
    }
    
    
}