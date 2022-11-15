using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;

namespace Hub.Shared.GoogleApi;

public static class CredentialProvider
{
    public static ServiceAccountCredential GetServiceAccountCredential(IConfiguration configuration, string[] scopes)
    {
        var googleCertificate = configuration.GetValue<string>("GoogleCertificate");
        var serviceAccountEmail = configuration.GetValue<string>("GoogleServiceAccountEmail");

        var certificate = new X509Certificate2(Convert.FromBase64String(googleCertificate),
            string.Empty,
            X509KeyStorageFlags.MachineKeySet |
            X509KeyStorageFlags.PersistKeySet |
            X509KeyStorageFlags.Exportable);

        var accountCredential = new ServiceAccountCredential(
            new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes
            }.FromCertificate(certificate));

        return accountCredential;
    }
}