using System.Globalization;
using JetBrains.Annotations;
using MudBlazor;

namespace Hub.Shared.Web.BlazorServer;

[UsedImplicitly]
public static class Constants
{
    [UsedImplicitly]
    public static CultureInfo CultureInfoNorway = CultureInfo.GetCultureInfo("nb-NO");
    
    [UsedImplicitly]
    public static Variant InputVariant = Variant.Outlined;
    
    [UsedImplicitly]
    public static Variant FormButtonVariant = Variant.Filled;

    [UsedImplicitly]
    public static string FormInputCol = "col-lg-5 col-sm-12";
}