namespace Lexicom.Mvvm;

public interface IAsyncMessageReply
{
    Task SendAsync(CancellationToken cancellationToken);
}
public class AsyncMessageReply<TMessage> : IAsyncMessageReply where TMessage : class
{
    /// <exception cref="ArgumentNullException"/>
    public AsyncMessageReply(
        TMessage message,
        IAsyncRecipient<TMessage> recipient)
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(recipient);

        Message = message;
        Recipient = recipient;
    }

    public TMessage Message { get; }
    public IAsyncRecipient<TMessage> Recipient { get; }

    public async Task SendAsync(CancellationToken cancellationToken)
    {
        await Recipient.ReceiveAsync(Message, cancellationToken);
    }
}
