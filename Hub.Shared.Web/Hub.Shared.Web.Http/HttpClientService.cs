using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Hub.Shared.Web.Http
{
    [UsedImplicitly]
    public abstract class HttpClientService
    {
        [UsedImplicitly]
        protected readonly HttpClient HttpClient;
        private DateTime? _tokenExpirationDate;
        
        [UsedImplicitly]
        public string FriendlyApiName { get; }
        
        [UsedImplicitly]
        protected bool IsAuthenticated => _tokenExpirationDate != null && _tokenExpirationDate < DateTime.Now;
        
        protected HttpClientService(HttpClient httpClient, 
            string friendlyApiName)
        {
            HttpClient = httpClient;
            FriendlyApiName = friendlyApiName;
        }

        [UsedImplicitly]
        protected async Task<TResponseObject> Get<TResponseObject>(string requestUri, params string[] requestParameters)
        {
            var response = await HttpClient.GetAsync(BuildGetRequest(requestUri, requestParameters));
            
            return await HandleResponse<TResponseObject>(response);
        }

        [UsedImplicitly]
        protected async Task RequestClientCredentialsTokenAsync(string address, string clientId, string secret)
        {
            var tokenResponse = await HttpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = address,
                ClientId = clientId,
                ClientSecret = secret
            });
            
            if (tokenResponse.IsError)
            {
                throw new HttpRequestException($"Request to {FriendlyApiName} ({tokenResponse.HttpResponse.RequestMessage.RequestUri}) failed: {tokenResponse.Error} ({tokenResponse.HttpStatusCode}).");
            }

            HttpClient.SetBearerToken(tokenResponse.AccessToken);
            
            _tokenExpirationDate = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn);
        }
        
        [UsedImplicitly]
        protected async Task<TResponseObject> Post<TResponseObject>(string requestUri, object payload)
        {
            var uri = new Uri(HttpClient.BaseAddress, requestUri);
            
            var payloadSerialized = JsonConvert.SerializeObject(payload);
            var stringContent = new StringContent(payloadSerialized, Encoding.UTF8, "application/json");
            
            var response = await HttpClient.PostAsync(uri, stringContent);

            return await HandlePostResponse<TResponseObject>(response);
        }

        private static string BuildGetRequest(string requestUri, string[] requestParameters)
        {
            if (requestParameters == null || !requestParameters.Any())
            {
                return requestUri;
            }

            var flattenedRequestParameters = string.Join("&", requestParameters);

            return $"{requestUri}?{flattenedRequestParameters}";
        }
        
        private async Task<TResponseObject> HandlePostResponse<TResponseObject>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<TResponseObject>();
            }

            throw new HttpRequestException($"Request to {FriendlyApiName} ({responseMessage.RequestMessage.RequestUri}) failed: {responseMessage.StatusCode}.");
        }

        private async Task<TResponseObject> HandleResponse<TResponseObject>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<TResponseObject>();
            }
            
            throw new HttpRequestException($"Request to {FriendlyApiName} ({responseMessage.RequestMessage.RequestUri}) failed: {responseMessage.StatusCode}.");
        }
    }
}