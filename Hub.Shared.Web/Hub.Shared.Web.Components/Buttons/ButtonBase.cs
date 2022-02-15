using Microsoft.AspNetCore.Components;

namespace Hub.Shared.Web.Components.Buttons;

public class ButtonBase : Component
{
    [Parameter]
    public bool SetDisabled { get; set; }
}