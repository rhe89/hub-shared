using Hub.Web.Http;
using Hub.Web.ViewModels;

namespace Hub.Web.Mapping
{
    public class ViewModelMappings
    {
        protected static THubViewModelBase GetErrorViewModel<THubViewModelBase, TResponseObject>(Response<TResponseObject> response) 
            where THubViewModelBase : ApiResponseViewModel, new()
        {
            return new THubViewModelBase
            {
                ErrorMessage = response.ErrorMessage,
                Success = false,
                StatusCode = response.StatusCode
            };
        }
    }
}