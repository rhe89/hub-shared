using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Hub.Web.Http
{
    public abstract class HttpClientService
    {
        protected readonly HttpClient HttpClient;
        
        public string FriendlyApiName { get; }

        protected HttpClientService(HttpClient httpClient, string friendlyApiName)
        {
            HttpClient = httpClient;
            FriendlyApiName = friendlyApiName;
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
            {
                return requestUri;
            }

            var flattenedRequestParameters = string.Join("&", requestParameters);

            return $"{requestUri}?{flattenedRequestParameters}";
        }
        
        private Response<TResponseObject> HandlePostResponse<TResponseObject>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return new Response<TResponseObject>
                {
                    StatusCode = responseMessage.StatusCode
                };
            }
            
            return new Response<TResponseObject>
            {
                StatusCode = responseMessage.StatusCode,
                ErrorMessage = $@"{FriendlyApiName} returned status code: {responseMessage.StatusCode}.
                            Request uri {responseMessage.RequestMessage.RequestUri}"
            };
        }

        private async Task<Response<TResponseObject>> HandleResponse<TResponseObject>(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                return new Response<TResponseObject>
                {
                    Data = await responseMessage.Content.ReadFromJsonAsync<TResponseObject>(),
                    StatusCode = responseMessage.StatusCode
                };
            }
            
            return new Response<TResponseObject>
            {
                StatusCode = responseMessage.StatusCode,
                ErrorMessage = $@"{FriendlyApiName} returned status code: {responseMessage.StatusCode}.
                            Request uri {responseMessage.RequestMessage.RequestUri}"
            };
        }

        private Response<TResponseObject> HandleRequestException<TResponseObject>(HttpRequestException exception, string requestUri)
        {
            return new Response<TResponseObject>
            {
                ErrorMessage = $"Error occured when requesting {FriendlyApiName} with uri {HttpClient.BaseAddress}{requestUri}: {exception.Message} {exception.InnerException?.Message}"
            };
        }

        private Response<TResponseObject> HandleResponseException<TResponseObject>(Exception exception, string requestUri)
        {
            return new Response<TResponseObject>
            {
                ErrorMessage = $"Error occured when handling response from {FriendlyApiName} with uri {HttpClient.BaseAddress}{requestUri}: {exception.InnerException?.Message}"
            };
        } 
    }
}