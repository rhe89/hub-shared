using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;
using MudBlazor;

namespace Hub.Shared.Web.BlazorServer.Services;

[UsedImplicitly]
public abstract class TableService<TQuery> 
    where TQuery : Query, new()
{
    [UsedImplicitly]
    public MudTable<TableRow> MudTableRef { get; set; }

    [UsedImplicitly]
    public IList<Column> HeaderRow { get; } = new List<Column>();

    [UsedImplicitly]
    public IList<Input> Filter { get; } = new List<Input>();

    [UsedImplicitly]
    public Column Footer { get; set; }
    
    [UsedImplicitly]
    public HashSet<TableRow> SelectedItems { get; set; } = new();
    
    [UsedImplicitly]
    public abstract Func<UIService, long, Task> OnRowClicked { get; }
    
    [UsedImplicitly]
    public int Columns => HeaderRow.Count;
    
    [UsedImplicitly]
    public bool Widget { get; set; }

    [UsedImplicitly]
    public abstract void CreateHeaderRow();
    
    [UsedImplicitly]
    public abstract Task<IList<TableRow>> FetchData(TQuery query, TableState tablestate);
    
    [UsedImplicitly]
    public abstract Task OpenAddItemDialog(UIService uiService);

    [UsedImplicitly]
    protected async Task ReloadServerData()
    {
        if (MudTableRef != null)
        {
            await MudTableRef.ReloadServerData();
        }
    }

    [UsedImplicitly]
    protected async Task OnItemAdded(UIService uiService, long id)
    {
        await ReloadServerData();
        
        uiService.ShowSnackbar("Item added", Severity.Success);
    }

    [UsedImplicitly]
    protected async Task OnItemUpdated(UIService uiService, long id)
    {
        await ReloadServerData();
        
        uiService.ShowSnackbar("Item saved", Severity.Success);
    }

    [UsedImplicitly]
    protected async Task OnItemDeleted(UIService uiService, long id)
    {
        await ReloadServerData();
        
        uiService.ShowSnackbar("Item deleted", Severity.Success);
    }
}

public class TableRow
{
    [UsedImplicitly]
    public long Id { get; init; }
    
    [UsedImplicitly]
    public IList<Column> Columns { get; init; }
}

public class Column
{
    public Column()
    {
        ChildElements = new List<ColumnText>();
    }

    [UsedImplicitly]
    public string TdClass { get; init; }
    
    [UsedImplicitly]
    public ColumnText ColumnText
    {
        get => ChildElements.FirstOrDefault();
        init => ChildElements.Add(value);
    }

    public IList<ColumnText> ChildElements { get; init; }
}

public class ColumnText
{
    [UsedImplicitly]
    public string Icon { get; init; }
    
    [UsedImplicitly]
    public string Text { get; init; }
    
    [UsedImplicitly]
    public string SmallText { get; init; }
    
    [UsedImplicitly]
    public string Class { get; init; }
    
    [UsedImplicitly]
    public Color Color { get; init; }
}

public class Input
{
    [UsedImplicitly]
    public bool Enabled { get; set; } = true;
    
    [UsedImplicitly]
    public FilterType FilterType { get; init; } = FilterType.TextField;
    
    [UsedImplicitly]
    public string Name { get; init; }
}

public class Checkbox<TQuery> : Input where TQuery : Query, new()
{
    [UsedImplicitly]
    public bool Value { get; set; }
    
    [UsedImplicitly]
    public Func<Checkbox<TQuery>, bool, TQuery, Task> OnChanged { get; init; }
}

public enum FilterType
{
    TextField,
    
    [UsedImplicitly]
    Select,
    
    [UsedImplicitly]
    Checkbox,
    
    [UsedImplicitly]
    Radio,
    
    [UsedImplicitly]
    Component
} 