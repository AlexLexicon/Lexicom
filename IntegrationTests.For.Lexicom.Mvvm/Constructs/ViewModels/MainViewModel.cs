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
    public partial HeaderViewModel HeaderViewModel { get; set; }

    [ObservableProperty]
    public partial NotificationTrayViewModel NotificationTrayViewModel { get; set; }

    [ObservableProperty]
    public partial StatusBarViewModel StatusBarViewModel { get; set; }

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
