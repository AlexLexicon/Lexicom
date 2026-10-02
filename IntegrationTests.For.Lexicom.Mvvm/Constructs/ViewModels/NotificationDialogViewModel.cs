using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Mvvm;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Messages;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.ViewModels;

public partial class NotificationDialogViewModel : DisposableObservableObject, IRecipient<NewNotificationMessage>
{
    [ObservableProperty]
    public int _receivedNotificationCount;

    public void Receive(NewNotificationMessage message)
    {
        ReceivedNotificationCount++;
    }
}
