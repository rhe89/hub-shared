using Hub.Web.Http;

namespace Hub.Web.ViewModels
{
    public class ViewModelMappings
    {
        public static THubViewModelBase GetErrorViewModel<THubViewModelBase, TResponseObject>(Response<TResponseObject> response) 
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