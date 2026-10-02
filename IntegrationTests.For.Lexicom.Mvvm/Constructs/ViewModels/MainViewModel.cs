using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Mvvm;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;

public partial class MainViewModel : DisposableObservableObject
{
    public MainViewModel(
        HeaderViewModel headerViewModel,
        NotificationTrayViewModel notificationTrayViewModel,
        StatusBarViewModel statusBarViewModel)
    {
        HeaderViewModel = headerViewModel;
        NotificationTrayViewModel = notificationTrayViewModel;
        StatusBarViewModel = statusBarViewModel;
    }

    [ObservableProperty]
    public HeaderViewModel _headerViewModel;

    [ObservableProperty]
    public NotificationTrayViewModel _notificationTrayViewModel;

    [ObservableProperty]
    public StatusBarViewModel _statusBarViewModel;

    public override void Dispose()
    {
        base.Dispose();

        HeaderViewModel?.Dispose();
        NotificationTrayViewModel?.Dispose();
        StatusBarViewModel?.Dispose();
    }

    public async Task LoadAsync()
    {
        await HeaderViewModel.LoadAsync();
        await NotificationTrayViewModel.LoadAsync();
        await StatusBarViewModel.LoadAsync();
    }
}
