using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Hub.Shared.Web.BlazorServer.Components;

public class BaseComponent : ComponentBase
{
    [UsedImplicitly]
    public bool Working { get; set; }
}