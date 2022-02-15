using Microsoft.AspNetCore.Components;

namespace Hub.Shared.Web.Components;

public class Component : ComponentBase
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
    
    [Parameter]
    public string? Class { get; set; }
    
    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }
}