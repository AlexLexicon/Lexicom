using Microsoft.AspNetCore.Components;
using System.ComponentModel;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
public abstract class View<TViewModel> : ComponentBase, IDisposable where TViewModel : INotifyPropertyChanged
{
    private TViewModel? _viewModel;
    [Inject]
    public TViewModel ViewModel
    {
        //we just have to trust that the Inject attribute will be before this is ever used
        get => _viewModel!; 
        set
        {
            RemoveViewModel();

            _viewModel = value;

            if (_viewModel is not null)
            {
                _viewModel.PropertyChanged += OnPropertyChanged;
            }
        }
    }

    public void Dispose()
    {
        RemoveViewModel();
    }

    private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        await InvokeAsync(StateHasChanged);
    }

    private void RemoveViewModel()
    {
        if (ViewModel is not null)
        {
            ViewModel.PropertyChanged -= OnPropertyChanged;
        }
    }
}