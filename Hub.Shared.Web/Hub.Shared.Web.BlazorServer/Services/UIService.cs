using System;
using System.Threading.Tasks;
using Hub.Shared.Web.BlazorServer.Extensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Services;

namespace Hub.Shared.Web.BlazorServer.Services;

public sealed class UIService : ComponentBase
{
    private readonly IDialogService _dialogService;
    private readonly IResizeService _resizeService;
    private readonly ISnackbar _snackbar;

    public UIService(
        IDialogService dialogService,
        IResizeService resizeService,
        ISnackbar snackbar)
    {
        _dialogService = dialogService;
        _resizeService = resizeService;
        _snackbar = snackbar;
    }
    
    [UsedImplicitly]
    public async Task ShowDialog<TDialog>()
        where TDialog : ComponentBase
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = await IsDesktop()};

        _dialogService.Show<TDialog>(null, options);
    }

    [UsedImplicitly]
    public async Task ShowDialog<TDialog>(DialogParameters dialogParameters)
        where TDialog : ComponentBase
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = await IsDesktop()};

        _dialogService.Show<TDialog>(null, dialogParameters, options);
    }
    
    [UsedImplicitly]
    public async Task ShowDialog(
        Type component,
        DialogParameters dialogParameters)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, FullScreen = await IsDesktop()};

        _dialogService.Show(component, null, dialogParameters, options);
    }
    
    [UsedImplicitly]
    public void ShowSnackbar(string message, Severity severity)
    {
        _snackbar.Add(message, severity, options =>
        {
            options.VisibleStateDuration = 2000;
            options.ShowTransitionDuration = 200;
            options.HideTransitionDuration = 200;
        });
    }

    public async Task<bool> IsDesktop()
    {
        return await _resizeService.IsDesktop();
    }
}