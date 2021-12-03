using System.Net;

namespace Hub.Shared.Web.Http
{
    public class Response<TResponseObject>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public TResponseObject Data { get; set; }
        public bool Success => StatusCode == HttpStatusCode.OK;
    }
}