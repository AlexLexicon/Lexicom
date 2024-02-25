using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
public abstract class ParameterView<TViewModel> : LayoutComponentBase, IView<TViewModel>, IDisposable where TViewModel : INotifyPropertyChanged
{
    private readonly ViewBehavior<TViewModel> _viewBehavior;

    public ParameterView()
    {
        _viewBehavior = new ViewBehavior<TViewModel>(this);
    }

    private TViewModel? _viewModel;
    [Parameter]
#pragma warning disable BL0007 // we are not updating the view in this code, only adding/removing event triggers so we should be safe
    public TViewModel ViewModel
    {
        get => _viewModel!; //technically _viewModel will be null if the implmentation doesnt set the parameter but in that case an execption will be thrown from 'OnInitializedAsync' and because of that we can actually say this is never null for the consuming implementation
        set
        {
            _viewBehavior.DisposeViewModel();

            _viewModel = value;

            _viewBehavior.ChangeViewModel();
        }
    }
#pragma warning restore BL0007

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