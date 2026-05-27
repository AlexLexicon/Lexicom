using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Mvvm;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Messages;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Models;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Services;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;

public partial class NotificationTrayViewModel : DisposableObservableObject, IAsyncRecipient<NewNotificationMessage>
{
    private readonly IAccountService _accountService;
    private readonly INotificationService _notificationService;

    public NotificationTrayViewModel(
        NotificationDialogViewModel notificationDialogViewModel,
        IAccountService accountService,
        INotificationService notificationService)
    {
        _accountService = accountService;
        _notificationService = notificationService;

        NotificationDialogViewModel = notificationDialogViewModel;
    }

    [ObservableProperty]
    public partial int NotificationsCount { get; set; }

    [ObservableProperty]
    public partial int ReceivedNotificationCount { get; set; }

    [ObservableProperty]
    public partial NotificationDialogViewModel NotificationDialogViewModel { get; set; }

    public override void Dispose()
    {
        base.Dispose();

        NotificationDialogViewModel.Dispose();
    }

    public async Task LoadAsync()
    {
        await UpdateNotificationsCountAsync();
    }

    public async Task ReceiveAsync(NewNotificationMessage message, CancellationToken cancellationToken)
    {
        ReceivedNotificationCount++;

        await UpdateNotificationsCountAsync();
    }

    private async Task UpdateNotificationsCountAsync()
    {
        Account account = await _accountService.GetLoggedInAccountAsync();

        NotificationsCount = await _notificationService.GetNotificationsCountAsync(account.ProfileId);
    }
}
