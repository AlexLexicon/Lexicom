using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Testing.Extensions;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Messages;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Services;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;
using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.For.Lexicom.Mvvm;

public class MessengerTests
{
    [Fact]
    public async Task Sending_Async_Message_Is_Received_By_ViewModels()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<HeaderViewModel>();
                mvvm.AddViewModel<MainViewModel>();
                mvvm.AddViewModel<NotificationDialogViewModel>();
                mvvm.AddViewModel<NotificationTrayViewModel>();
                mvvm.AddViewModel<ProfileViewModel>();
                mvvm.AddViewModel<StatusBarViewModel>();
            });
        });

        var notificationService = new NotificationService();

        ita.AddSingleton<IAccountService, AccountService>();
        ita.AddSingleton<INotificationService>(notificationService);

        //act
        var vm = ita.GetRequiredService<MainViewModel>();
        var messenger = ita.GetRequiredService<IMessenger>();

        await vm.LoadAsync();

        int? initialNotificationProfileCount = vm.HeaderViewModel?.ProfileViewModel?.NotificationsCount;
        int? initialNotificationTrayCount = vm.NotificationTrayViewModel?.NotificationsCount;

        notificationService.Count += 3;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int? laterNotificationProfileCount = vm.HeaderViewModel?.ProfileViewModel?.NotificationsCount;
        int? laterNotificationTrayCount = vm.NotificationTrayViewModel?.NotificationsCount;

        //assert
        Assert.NotNull(initialNotificationProfileCount);
        Assert.NotNull(initialNotificationTrayCount);
        Assert.NotNull(laterNotificationProfileCount);
        Assert.NotNull(laterNotificationTrayCount);

        Assert.Equal(5, initialNotificationProfileCount.Value);
        Assert.Equal(5, initialNotificationTrayCount.Value);
        Assert.Equal(8, laterNotificationProfileCount.Value);
        Assert.Equal(8, laterNotificationTrayCount.Value);
    }

    [Fact]
    public async Task Async_Message_Is_Received_By_Async_Recipients_And_Sync_Recipients()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<HeaderViewModel>();
                mvvm.AddViewModel<MainViewModel>();
                mvvm.AddViewModel<NotificationDialogViewModel>();
                mvvm.AddViewModel<NotificationTrayViewModel>();
                mvvm.AddViewModel<ProfileViewModel>();
                mvvm.AddViewModel<StatusBarViewModel>();
            });
        });

        var notificationService = new NotificationService();

        ita.AddSingleton<IAccountService, AccountService>();
        ita.AddSingleton<INotificationService>(notificationService);

        //act
        var vm = ita.GetRequiredService<MainViewModel>();
        var messenger = ita.GetRequiredService<IMessenger>();

        await vm.LoadAsync();

        int initialReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int initialReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int firstReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int firstReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int secondReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int secondReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        //assert
        Assert.Equal(0, initialReceivedNotificationTrayCount);
        Assert.Equal(0, initialReceivedNotificationDialogCount);

        Assert.Equal(1, firstReceivedNotificationTrayCount);
        Assert.Equal(1, firstReceivedNotificationDialogCount);

        Assert.Equal(2, secondReceivedNotificationTrayCount);
        Assert.Equal(2, secondReceivedNotificationDialogCount);
    }

    [Fact]
    public async Task Only_Send_Sync_Message_To_Sync_Recipient_Even_When_Async_Is_Registered()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<HeaderViewModel>();
                mvvm.AddViewModel<MainViewModel>();
                mvvm.AddViewModel<NotificationDialogViewModel>();
                mvvm.AddViewModel<NotificationTrayViewModel>();
                mvvm.AddViewModel<ProfileViewModel>();
                mvvm.AddViewModel<StatusBarViewModel>();
            });
        });

        var notificationService = new NotificationService();

        ita.AddSingleton<IAccountService, AccountService>();
        ita.AddSingleton<INotificationService>(notificationService);

        //act
        var vm = ita.GetRequiredService<MainViewModel>();
        var messenger = ita.GetRequiredService<IMessenger>();

        await vm.LoadAsync();

        int initialReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int initialReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        messenger.Send(new NewNotificationMessage());

        int firstReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int firstReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        //assert
        Assert.Equal(0, initialReceivedNotificationTrayCount);
        Assert.Equal(0, initialReceivedNotificationDialogCount);

        Assert.Equal(0, firstReceivedNotificationTrayCount);
        Assert.Equal(1, firstReceivedNotificationDialogCount);
    }

    [Fact]
    public async Task Sending_Sync_Is_Only_Received_By_Sync_Recipient_ViewModels()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<StatusBarViewModel>();
            });
        });

        //act
        var vm = ita.Make<StatusBarViewModel>();
        var messenger = ita.GetRequiredService<IMessenger>();

        await vm.LoadAsync();

        int initialAsyncCount = vm.AsyncReceivedCount;
        int ititialSyncCount = vm.SyncReceivedCount;

        await messenger.SendAsync(new StatusMessage(), TestContext.Current.CancellationToken);

        int asyncAsyncCount = vm.AsyncReceivedCount;
        int asyncSyncCount = vm.SyncReceivedCount;

        messenger.Send(new StatusMessage());

        int syncAsyncCount = vm.AsyncReceivedCount;
        int syncSyncCount = vm.SyncReceivedCount;

        //assert
        Assert.Equal(0, initialAsyncCount);
        Assert.Equal(0, ititialSyncCount);

        Assert.Equal(1, asyncAsyncCount);
        Assert.Equal(1, asyncSyncCount);

        Assert.Equal(1, syncAsyncCount);
        Assert.Equal(2, syncSyncCount);
    }

    [Fact]
    public async Task Disposed_Recipients_Do_Not_Receive_Messages()
    {
        //arrange
        var ita = new IntegrationTestAssistant();

        ita.TestLexicom(l =>
        {
            l.AddMvvm(mvvm =>
            {
                mvvm.AddViewModel<NotificationDialogViewModel>();
                mvvm.AddViewModel<NotificationTrayViewModel>();
            });
        });

        ita.AddSingleton<IAccountService, AccountService>();

        //act
        var vm = ita.Make<NotificationTrayViewModel>();
        var messenger = ita.GetRequiredService<IMessenger>();

        await vm.LoadAsync();

        int initialReceivedNotificationTrayCount = vm.ReceivedNotificationCount;
        int initialReceivedNotificationDialogCount = vm.NotificationDialogViewModel.ReceivedNotificationCount;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int firstReceivedNotificationTrayCount = vm.ReceivedNotificationCount;
        int firstReceivedNotificationDialogCount = vm.NotificationDialogViewModel.ReceivedNotificationCount;

        vm.Dispose();

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int secondReceivedNotificationTrayCount = vm.ReceivedNotificationCount;
        int secondReceivedNotificationDialogCount = vm.NotificationDialogViewModel.ReceivedNotificationCount;

        //assert
        Assert.Equal(0, initialReceivedNotificationTrayCount);
        Assert.Equal(0, initialReceivedNotificationDialogCount);

        Assert.Equal(1, firstReceivedNotificationTrayCount);
        Assert.Equal(1, firstReceivedNotificationDialogCount);

        Assert.Equal(1, secondReceivedNotificationTrayCount);
        Assert.Equal(1, secondReceivedNotificationDialogCount);
    }
}
