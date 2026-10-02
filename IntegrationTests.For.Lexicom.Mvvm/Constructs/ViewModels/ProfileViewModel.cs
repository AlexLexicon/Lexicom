using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Mvvm;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Messages;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Models;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Services;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;

public partial class ProfileViewModel : DisposableObservableObject, IAsyncRecipient<NewNotificationMessage>
{
    private readonly IAccountService _accountService;
    private readonly INotificationService _notificationService;

    public ProfileViewModel(
        Account account,
        IAccountService accountService,
        INotificationService notificationService)
    {
        _accountService = accountService;
        _notificationService = notificationService;

        Account = account;
    }

    private Account Account { get; }

    [ObservableProperty]
    public partial string? Name { get; set; }

    [ObservableProperty]
    public partial int NotificationsCount { get; set; }

    public async Task LoadAsync()
    {
        Name = await _accountService.GetProfileNameAsync(Account.ProfileId);

        await UpdateNotificationCountAsync();
    }

    public async Task ReceiveAsync(NewNotificationMessage message, CancellationToken cancellationToken)
    {
        await UpdateNotificationCountAsync();
    }

    private async Task UpdateNotificationCountAsync()
    {
        NotificationsCount = await _notificationService.GetNotificationsCountAsync(Account.ProfileId);
    }
}
