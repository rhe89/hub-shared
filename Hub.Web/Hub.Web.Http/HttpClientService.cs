using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hub.Web.Http
{
    public abstract class HttpClientService
    {
        protected readonly HttpClient HttpClient;
        private readonly ILogger<HttpClientService> _logger;
        private readonly string _friendlyApiName;

        protected HttpClientService(HttpClient httpClient, ILogger<HttpClientService> logger, string friendlyApiName)
        {
            HttpClient = httpClient;
            _logger = logger;
            _friendlyApiName = friendlyApiName;
        }

        protected async Task<Response<TResponseObject>> Get<TResponseObject>(string requestUri, params string[] requestParameters)
        {
            try
            {
                var response = await HttpClient.GetAsync(BuildGetRequest(requestUri, requestParameters));

                return await HandleResponse<TResponseObject>(response);
            }
            catch (HttpRequestException e)
            {
                return HandleRequestException<TResponseObject>(e, requestUri);
            }
            catch (Exception e)
            {
                return HandleResponseException<TResponseObject>(e, requestUri);
            }
        }
        
        protected async Task<Response<string>> Post(string requestUri, object payload)
        {
            var uri = new Uri(HttpClient.BaseAddress, requestUri);
            
            try
            {
                var payloadSerialized = JsonConvert.SerializeObject(payload);
                var stringContent = new StringContent(payloadSerialized, Encoding.UTF8, "application/json");
                
                var response = await HttpClient.PostAsync(uri, stringContent);

                return HandlePostResponse<string>(response);
            }
            catch (HttpRequestException e)
            {
                return HandleRequestException<string>(e, uri.OriginalString);
            }
            catch (Exception e)
            {
                return HandleResponseException<string>(e, uri.OriginalString);
            }
        }

        private static string BuildGetRequest(string requestUri, string[] requestParameters)
        {
            if (requestParameters == null || !requestParameters.Any())
                return requestUri;

            var flattenedRequestParameters = string.Join("&", requestParameters);

            return $"{requestUri}?{flattenedRequestParameters}";
        }
        
        private Response<TResponseObject> HandlePostResponse<TResponseObject>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
                return new Response<TResponseObject>
                {
                    StatusCode = responseMessage.StatusCode
                };
            
            _logger.LogError(
                $@"{_friendlyApiName} returned status code: {responseMessage.StatusCode}.
                            Request uri {responseMessage.RequestMessage.RequestUri}");

            return new Response<TResponseObject>
            {
                StatusCode = responseMessage.StatusCode,
                ErrorMessage = "Error occured when calling API."
            };
        }

        private async Task<Response<TResponseObject>> HandleResponse<TResponseObject>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
                return new Response<TResponseObject>
                {
                    Data = await responseMessage.Content.ReadFromJsonAsync<TResponseObject>(),
                    StatusCode = responseMessage.StatusCode
                };
            
            _logger.LogError(
                $@"{_friendlyApiName} returned status code: {responseMessage.StatusCode}.
                            Request uri {responseMessage.RequestMessage.RequestUri}");

            return new Response<TResponseObject>
            {
                StatusCode = responseMessage.StatusCode,
                ErrorMessage = "Error occured when calling API."
            };
        }

        private Response<TResponseObject> HandleRequestException<TResponseObject>(HttpRequestException exception, string requestUri)
        {
            _logger.LogError(
                $"Error occured when requesting {_friendlyApiName} with uri {HttpClient.BaseAddress}{requestUri}: {exception.Message} {exception.InnerException?.Message}");

            return new Response<TResponseObject>
            {
                ErrorMessage = $"Error occured in request when calling API. Error message: {exception.Message}"
            };
        }

        private Response<TResponseObject> HandleResponseException<TResponseObject>(Exception exception, string requestUri)
        {
            _logger.LogError(
                $"Error occured when handling response from {_friendlyApiName} with uri {HttpClient.BaseAddress}{requestUri}: {exception.InnerException?.Message}");
            
            return new Response<TResponseObject>
            {
                ErrorMessage = $"Error when handling response from API. Error message: {exception.Message}"
            };
        } 
    }
}