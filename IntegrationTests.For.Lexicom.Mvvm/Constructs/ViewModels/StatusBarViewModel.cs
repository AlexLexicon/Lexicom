using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Mvvm;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Messages;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;

public partial class StatusBarViewModel : DisposableObservableObject, IAsyncRecipient<StatusMessage>, IRecipient<StatusMessage>
{
    [ObservableProperty]
    public partial int AsyncReceivedCount { get; set; }

    [ObservableProperty]
    public partial int SyncReceivedCount { get; set; }

    public Task LoadAsync()
    {
        return Task.CompletedTask;
    }

    public Task ReceiveAsync(StatusMessage message, CancellationToken cancellationToken)
    {
        AsyncReceivedCount++;

        return Task.CompletedTask;
    }

    public void Receive(StatusMessage message)
    {
        SyncReceivedCount++;
    }
}
