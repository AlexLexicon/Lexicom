using CommunityToolkit.Mvvm.Input;
using Lexicom.Mvvm.For.Blazor.WebAssembly.Exceptions;
using Microsoft.AspNetCore.Components;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

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

                CacheViewModelProperties();
            }
        }
    }

    private PropertyInfo? LoadedCommand { get; set; }
    private List<PropertyInfo> NotifyCollectionChangedProperties { get; } = [];

    public void Dispose()
    {
        RemoveViewModel();
    }

    protected override async Task OnInitializedAsync()
    {
        if (ViewModel is not null && LoadedCommand is not null)
        {
            object? loadedCommandPropertyValue = LoadedCommand.GetValue(ViewModel);

            if (loadedCommandPropertyValue is IAsyncRelayCommand loadedAsyncCommand)
            {
                await loadedAsyncCommand.ExecuteAsync(null);
            }
            else if (loadedCommandPropertyValue is ICommand loadedCommand)
            {
                loadedCommand.Execute(null);
            }
            else
            {
                throw new LoadedCommandNotValidException(ViewModel);
            }
        }

        await base.OnInitializedAsync();
    }

    private void RemoveViewModel()
    {
        if (ViewModel is not null)
        {
            UnSubscribeToCollectionChanged();

            ViewModel.PropertyChanged -= OnPropertyChanged;
        }
    }

    private void CacheViewModelProperties()
    {
        PropertyInfo[] properties = ViewModel
            .GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo property in properties)
        {
            if (property.Name is Constants.COMMAND_LOADED)
            {
                LoadedCommand = property;
            }
            else if (property.PropertyType.IsAssignableTo(typeof(INotifyCollectionChanged)))
            {
                NotifyCollectionChangedProperties.Add(property);
            }
        }

        SubscribeToCollectionChanged();
    }

    private void UnSubscribeToCollectionChanged()
    {
        foreach (PropertyInfo notifyCollectionChangedProperty in NotifyCollectionChangedProperties)
        {
            object? value = notifyCollectionChangedProperty.GetValue(ViewModel);

            if (value is not null and INotifyCollectionChanged notifyCollectionChanged)
            {
                notifyCollectionChanged.CollectionChanged -= OnCollectionChanged;

            }
        }
    }

    private void SubscribeToCollectionChanged()
    {
        foreach (PropertyInfo notifyCollectionChangedProperty in NotifyCollectionChangedProperties)
        {
            object? value = notifyCollectionChangedProperty.GetValue(ViewModel);

            if (value is not null and INotifyCollectionChanged notifyCollectionChanged)
            {
                notifyCollectionChanged.CollectionChanged += OnCollectionChanged;
            }
        }
    }

    private async Task InvokeStateChange()
    {
        await InvokeAsync(StateHasChanged);
    }

    private async void OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (ViewModel is not null)
        {
            SubscribeToCollectionChanged();
        }

        await InvokeStateChange();
    }

    private async void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        await InvokeStateChange();
    }
}