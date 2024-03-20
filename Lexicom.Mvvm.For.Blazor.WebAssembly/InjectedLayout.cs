using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
public class InjectedLayout<TViewModel> : LayoutComponentBase, IMvvmComponent<TViewModel>, IDisposable where TViewModel : INotifyPropertyChanged
{
    private readonly ComponentBehavior<TViewModel> _componentBehavior;

    public InjectedLayout()
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

    public virtual void Dispose()
    {
        _componentBehavior.DisposeViewModel();
    }

    public virtual async Task InvokeStateChangeAsync()
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