using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;

public abstract class ParameterLayout<TViewModel> : LayoutComponentBase, IMvvmComponent<TViewModel>, IDisposable where TViewModel : INotifyPropertyChanged
{
    private readonly ComponentBehavior<TViewModel> _componentBehavior;

    public ParameterLayout()
    {
        _componentBehavior = new ComponentBehavior<TViewModel>(this);
    }

    [Parameter]
    public TViewModel ViewModel
    {
        get => field!; //technically _viewModel will be null if the implementation doesnt set the parameter but in that case an exception will be thrown from 'OnInitializedAsync' and because of that we can actually say this is never null for the consuming implementation
        set
        {
            _componentBehavior.DisposeViewModel();

            field = value;

            _componentBehavior.SubmitViewModel();
        }
    }

    public void Dispose()
    {
        _componentBehavior.DisposeViewModel();
    }

    public async Task InvokeStateChangeAsync()
    {
        await InvokeAsync(StateHasChanged);
    }

    public async Task HandleExceptionAsync(Exception exception)
    {
        await DispatchExceptionAsync(exception);
    }

    protected override async Task OnInitializedAsync()
    {
        await _componentBehavior.InitializeAsync();

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await _componentBehavior.AfterRenderAsync(firstRender);

        await base.OnAfterRenderAsync(firstRender);
    }
}