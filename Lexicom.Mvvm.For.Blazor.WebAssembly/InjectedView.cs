using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
public abstract class InjectedView<TViewModel> : ComponentBase, IView<TViewModel>, IDisposable where TViewModel : INotifyPropertyChanged
{
    private readonly ViewBehavior<TViewModel> _viewBehavior;

    public InjectedView()
    {
        _viewBehavior = new ViewBehavior<TViewModel>(this);
    }

    private TViewModel? _viewModel;
    [Inject]
    public TViewModel ViewModel
    {
        get => _viewModel!; //we just have to trust that the Inject attribute will be before this is ever used
        set
        {
            _viewBehavior.DisposeViewModel();

            _viewModel = value;

            _viewBehavior.ChangeViewModel();
        }
    }

    public void Dispose()
    {
        _viewBehavior.DisposeViewModel();
    }

    public async Task InvokeStateChange()
    {
        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await _viewBehavior.InitializeAsync();

        await base.OnInitializedAsync();
    }
}