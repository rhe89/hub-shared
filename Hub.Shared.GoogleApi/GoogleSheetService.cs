using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Configuration;

namespace Hub.Shared.GoogleApi;

public static class GoogleSheetService
{
    public static async Task UpdateValuesInTab(IConfiguration configuration, string spreadsheetId, string range, IList<IList<object>> values)
    {
        var updatedValues = new ValueRange
        {
            MajorDimension = "ROWS",
            Values = values,
            Range = range
        };

        var sheetsService = GetSheetsService(configuration);

        var update = sheetsService.Spreadsheets.Values.Update(updatedValues, spreadsheetId, range);

        update.ValueInputOption =
            SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;

        await update.ExecuteAsync();
    }

    public static async Task<IList<IList<object>>> GetValuesInTab(IConfiguration configuration, string spreadsheetId, string range)
    {
        var sheetsService = GetSheetsService(configuration);

        var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
        
        var response = await request.ExecuteAsync();

        return response?.Values;
    }

    public static SpreadsheetsResource.ValuesResource.GetRequest GetSpreadsheetRequest(IConfiguration configuration, string spreadsheetId, string range)
    {
        var sheetsService = GetSheetsService(configuration);

        return sheetsService.Spreadsheets.Values.Get(spreadsheetId, range);
    }
    
    public static SheetsService GetSheetsService(IConfiguration configuration)
    {
        var serverCredentials = CredentialProvider.GetServiceAccountCredential(configuration,new [] {SheetsService.Scope.Spreadsheets });

        var sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = serverCredentials,
            ApplicationName = "Hub"
        });
        
        return sheetsService;
    }
}