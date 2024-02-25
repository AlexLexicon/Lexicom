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

    private TViewModel? _viewModel;
    [Parameter]
#pragma warning disable BL0007 // we are not updating the view in this code, only adding/removing event triggers so we should be safe
    public TViewModel ViewModel
    {
        get => _viewModel!; //technically _viewModel will be null if the implmentation doesnt set the parameter but in that case an execption will be thrown from 'OnInitializedAsync' and because of that we can actually say this is never null for the consuming implementation
        set
        {
            _componentBehavior.DisposeViewModel();

            _viewModel = value;

            _componentBehavior.ChangeViewModel();
        }
    }
#pragma warning restore BL0007

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
}