using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Mvvm;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Messages;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;

public partial class StatusBarViewModel : DisposableObservableObject, IAsyncRecipient<StatusMessage>, IRecipient<StatusMessage>
{
    [ObservableProperty]
    public int _asyncReceivedCount;

    [ObservableProperty]
    public int _syncReceivedCount;

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
