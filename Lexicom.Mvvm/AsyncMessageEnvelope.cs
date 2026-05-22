using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Lexicom.Mvvm;

public class AsyncMessageEnvelope<TMessage> : CollectionRequestMessage<IAsyncMessageReply> where TMessage : class
{
    /// <exception cref="ArgumentNullException"/>
    public AsyncMessageEnvelope(TMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        Message = message;
    }

    public TMessage Message { get; }
}
