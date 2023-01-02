using System.Threading.Tasks;
using MudBlazor.Services;

namespace Hub.Shared.Web.BlazorServer.Extensions;

public static class ResizeServiceExtensions
{
    public static async Task<bool> IsDesktop(this IResizeService resizeService)
    {
        var windowSize = await resizeService.GetBrowserWindowSize();

        return windowSize.Width > 600;
    }
}