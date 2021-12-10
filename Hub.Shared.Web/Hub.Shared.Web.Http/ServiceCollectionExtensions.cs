using System;
using System.Net.Http;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Shared.Web.Http
{
    [UsedImplicitly]
    public static class ServiceCollectionExtensions
    {
        [UsedImplicitly]
        public static void AddHubHttpClient<TClient, TImplementation>(this IServiceCollection serviceCollection)
            where TClient : class
            where TImplementation : class, TClient
        {
            serviceCollection.AddHttpClient<TClient, TImplementation>();
        }
        
        [UsedImplicitly]
        public static void AddHubHttpClient<TClient, TImplementation>(this IServiceCollection serviceCollection, Action<HttpClient> configureClient)
            where TClient : class
            where TImplementation : class, TClient
        {
            serviceCollection.AddHttpClient<TClient, TImplementation>(configureClient);
        }
    }
}