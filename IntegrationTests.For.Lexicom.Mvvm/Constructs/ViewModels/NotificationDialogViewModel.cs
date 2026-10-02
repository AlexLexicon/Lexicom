using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using IntegrationTests.For.Lexicom.Mvvm.Constructs.Messages;
using Lexicom.Mvvm;

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
