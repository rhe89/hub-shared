using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Web.Http
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHubHttpClient<TClient, TImplementation>(this IServiceCollection serviceCollection)
            where TClient : class
            where TImplementation : class, TClient
        {
            serviceCollection.AddHttpClient<TClient, TImplementation>();
        }
        
        public static void AddHubHttpClient<TClient, TImplementation>(this IServiceCollection serviceCollection, Action<HttpClient> configureClient)
            where TClient : class
            where TImplementation : class, TClient
        {
            serviceCollection.AddHttpClient<TClient, TImplementation>(configureClient);
        }
    }
}