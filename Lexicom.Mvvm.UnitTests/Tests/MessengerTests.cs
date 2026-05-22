using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.For.Testing.Extensions;
using Lexicom.Mvvm.UnitTests.Constructs.Messages;
using Lexicom.Mvvm.UnitTests.Constructs.Services;
using Lexicom.Mvvm.UnitTests.Constructs.ViewModels;
using Lexicom.Supports.Testing.Extensions;
using Lexicom.Testing.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.UnitTests.Tests;

public class MessengerTests
{
    [Fact]
    public async Task Sending_Async_Message_Is_Recived_By_ViewModels()
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

        int? initalNotificationProfileCount = vm.HeaderViewModel?.ProfileViewModel?.NotificationsCount;
        int? initalNotificationTrayCount = vm.NotificationTrayViewModel?.NotificationsCount;

        notificationService.Count += 3;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int? laterNotificationProfileCount = vm.HeaderViewModel?.ProfileViewModel?.NotificationsCount;
        int? laterNotificationTrayCount = vm.NotificationTrayViewModel?.NotificationsCount;

        //assert
        Assert.NotNull(initalNotificationProfileCount);
        Assert.NotNull(initalNotificationTrayCount);
        Assert.NotNull(laterNotificationProfileCount);
        Assert.NotNull(laterNotificationTrayCount);

        Assert.Equal(5, initalNotificationProfileCount.Value);
        Assert.Equal(5, initalNotificationTrayCount.Value);
        Assert.Equal(8, laterNotificationProfileCount.Value);
        Assert.Equal(8, laterNotificationTrayCount.Value);
    }

    [Fact]
    public async Task Async_Message_Is_Recived_By_Async_Recipients_And_Sync_Recipents()
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

        int initalReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int initalReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int firstReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int firstReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int secondReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int secondReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        //assert
        Assert.Equal(0, initalReceivedNotificationTrayCount);
        Assert.Equal(0, initalReceivedNotificationDialogCount);

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

        int initalReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int initalReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        messenger.Send(new NewNotificationMessage());

        int firstReceivedNotificationTrayCount = vm.NotificationTrayViewModel.ReceivedNotificationCount;
        int firstReceivedNotificationDialogCount = vm.NotificationTrayViewModel.NotificationDialogViewModel.ReceivedNotificationCount;

        //assert
        Assert.Equal(0, initalReceivedNotificationTrayCount);
        Assert.Equal(0, initalReceivedNotificationDialogCount);

        Assert.Equal(0, firstReceivedNotificationTrayCount);
        Assert.Equal(1, firstReceivedNotificationDialogCount);
    }

    [Fact]
    public async Task Sending_Sync_Is_Only_Recived_By_Sync_Recipient_ViewModels()
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

        int initalAsyncCount = vm.AsyncRecievedCount;
        int ititalSyncCount = vm.SyncRecievedCount;

        await messenger.SendAsync(new StatusMessage(), TestContext.Current.CancellationToken);

        int AsyncAsyncCount = vm.AsyncRecievedCount;
        int AsyncSyncCount = vm.SyncRecievedCount;

        messenger.Send(new StatusMessage());

        int SyncAsyncCount = vm.AsyncRecievedCount;
        int SyncSyncCount = vm.SyncRecievedCount;

        //assert
        Assert.Equal(0, initalAsyncCount);
        Assert.Equal(0, ititalSyncCount);

        Assert.Equal(1, AsyncAsyncCount);
        Assert.Equal(1, AsyncSyncCount);

        Assert.Equal(1, SyncAsyncCount);
        Assert.Equal(2, SyncSyncCount);
    }

    [Fact]
    public async Task Disposed_Recipients_Do_Not_Recieve_Messaeges()
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

        int initalReceivedNotificationTrayCount = vm.ReceivedNotificationCount;
        int initalReceivedNotificationDialogCount = vm.NotificationDialogViewModel.ReceivedNotificationCount;

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int firstReceivedNotificationTrayCount = vm.ReceivedNotificationCount;
        int firstReceivedNotificationDialogCount = vm.NotificationDialogViewModel.ReceivedNotificationCount;

        vm.Dispose();

        await messenger.SendAsync(new NewNotificationMessage(), TestContext.Current.CancellationToken);

        int secondReceivedNotificationTrayCount = vm.ReceivedNotificationCount;
        int secondReceivedNotificationDialogCount = vm.NotificationDialogViewModel.ReceivedNotificationCount;

        //assert
        Assert.Equal(0, initalReceivedNotificationTrayCount);
        Assert.Equal(0, initalReceivedNotificationDialogCount);

        Assert.Equal(1, firstReceivedNotificationTrayCount);
        Assert.Equal(1, firstReceivedNotificationDialogCount);

        Assert.Equal(1, secondReceivedNotificationTrayCount);
        Assert.Equal(1, secondReceivedNotificationDialogCount);
    }
}
