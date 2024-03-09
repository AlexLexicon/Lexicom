using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
public abstract class InjectedPage<TViewModel> : ComponentBase, IMvvmComponent<TViewModel>, IDisposable where TViewModel : INotifyPropertyChanged
{
    private readonly ComponentBehavior<TViewModel> _componentBehavior;

    public InjectedPage()
    {
        _componentBehavior = new ComponentBehavior<TViewModel>(this);
    }

    private TViewModel? _viewModel;
    [Inject]
    public TViewModel ViewModel
    {
        get => _viewModel!; //we just have to trust that the Inject attribute will be before this is ever used
        set
        {
            _componentBehavior.DisposeViewModel();

            _viewModel = value;

            _componentBehavior.ChangeViewModel();
        }
    }

    public void Dispose()
    {
        _componentBehavior.DisposeViewModel();
    }

    public async Task InvokeStateChange()
    {
        await InvokeAsync(StateHasChanged);
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