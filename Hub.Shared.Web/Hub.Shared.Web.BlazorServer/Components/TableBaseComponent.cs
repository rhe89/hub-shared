using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;

namespace Hub.Shared.Web.BlazorServer.Components;

[UsedImplicitly]
public class TableBaseComponent<TQuery> : BaseComponent
    where TQuery : Query, new()
{
    [CascadingParameter(Name = "Query")]
    public TQuery Query { get; set; } = new();
    
    [Parameter]
    public int Take { get; set; }
    
    [Parameter]
    public bool Widget { get; set; }
}