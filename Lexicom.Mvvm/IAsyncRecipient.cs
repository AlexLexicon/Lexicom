namespace Lexicom.Mvvm;

public interface IAsyncRecipient<TMessage> where TMessage : class
{
    Task ReceiveAsync(TMessage message, CancellationToken cancellationToken);
}
