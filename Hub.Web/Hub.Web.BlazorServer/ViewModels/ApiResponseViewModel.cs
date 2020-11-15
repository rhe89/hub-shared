using System.Net;

namespace Hub.Web.BlazorServer.ViewModels
{
    public abstract class ApiResponseViewModel
    {
        public bool Success { get; set; }
        public bool Loading { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}