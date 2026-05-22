using CommunityToolkit.Mvvm.Messaging;

namespace Lexicom.Mvvm;

public class AsyncMessenger : IMessenger
{
    private readonly WeakReferenceMessenger _messenger;
    private readonly IMessengerScheduler _messengerScheduler;

    /// <exception cref="ArgumentNullException"/>
    public AsyncMessenger(
        WeakReferenceMessenger messenger, 
        IMessengerScheduler messengerScheduler)
    {
        ArgumentNullException.ThrowIfNull(messenger);
        ArgumentNullException.ThrowIfNull(messengerScheduler);

        _messenger = messenger;
        _messengerScheduler = messengerScheduler;
    }

    public void Cleanup()
    {
        _messenger.Cleanup();
    }

    public void Reset()
    {
        _messenger.Reset();
    }

    /// <exception cref="ArgumentNullException"/>
    public bool IsRegistered<TMessage, TToken>(object recipient, TToken token) where TMessage : class where TToken : IEquatable<TToken>
    {
        ArgumentNullException.ThrowIfNull(recipient);
        ArgumentNullException.ThrowIfNull(token);

        return _messenger.IsRegistered<TMessage, TToken>(recipient, token);
    }

    /// <exception cref="ArgumentNullException"/>
    public void Register<TRecipient, TMessage, TToken>(TRecipient recipient, TToken token, MessageHandler<TRecipient, TMessage> handler) where TRecipient : class where TMessage : class where TToken : IEquatable<TToken>
    {
        ArgumentNullException.ThrowIfNull(recipient);
        ArgumentNullException.ThrowIfNull(token);
        ArgumentNullException.ThrowIfNull(handler);

        _messenger.Register<TRecipient, TMessage, TToken>(recipient, token, (r, message) =>
        {
            if (recipient is DisposableObservableObject disposable && disposable.IsDisposed)
            {
                return;
            }

            handler.Invoke(recipient, message);
        });
    }

    public void AsyncRegister<TMessage>(IAsyncRecipient<TMessage> recipient) where TMessage : class
    {
        IMessengerExtensions.Register<AsyncMessageEnvelope<TMessage>>(this, recipient, (r, envelope) =>
        {
            if (r is IAsyncRecipient<TMessage> asyncRecipient)
            {
                if (asyncRecipient is DisposableObservableObject disposable && disposable.IsDisposed)
                {
                    return;
                }

                //normally you would execute a send here in the register handler function
                //however since we want to handle an async call we need to be able to await
                //it which we cant do here since this is a sync method. so we use a reply
                //to simply bundle the recipient and message together for sending later in
                //the calling SendAsync method which is async itself.

                var reply = new AsyncMessageReply<TMessage>(envelope.Message, asyncRecipient);

                envelope.Reply(reply);
            }
        });
    }

    /// <exception cref="ArgumentNullException"/>
    public TMessage Send<TMessage, TToken>(TMessage message, TToken token) where TMessage : class where TToken : IEquatable<TToken>
    {
        ArgumentNullException.ThrowIfNull(message);
        ArgumentNullException.ThrowIfNull(token);

        return _messenger.Send(message, token);
    }

    /// <exception cref="ArgumentNullException"/>
    public async Task SendAsync<TMessage>(TMessage message, AsyncMessageAwaitStrategy asyncMessageAwaitStrategy, CancellationToken cancellationToken = default) where TMessage : class
    {
        ArgumentNullException.ThrowIfNull(message);

        //send the sync message
        IMessengerExtensions.Send(this, message);

        //send the async message
        var envelope = new AsyncMessageEnvelope<TMessage>(message);

        IMessengerExtensions.Send(this, envelope);

        if (asyncMessageAwaitStrategy == AsyncMessageAwaitStrategy.ForeachAwait)
        {
            foreach (IAsyncMessageReply reply in envelope.Responses)
            {
                await reply.SendAsync(cancellationToken);
            }
        }
        else
        {
            var tasks = new HashSet<Task>();
            foreach (IAsyncMessageReply reply in envelope.Responses)
            {
                Task sendTask = reply.SendAsync(cancellationToken);

                tasks.Add(sendTask);
            }

            await Task.WhenAll(tasks);
        }
    }

    public async Task ScheduleAsync<TMessage>(TMessage message, ScheduleMessagePriority scheduleMessagePriority, AsyncMessageAwaitStrategy asyncMessageAwaitStrategy, CancellationToken cancellationToken = default) where TMessage : class
    {
        ArgumentNullException.ThrowIfNull(message);

        await _messengerScheduler.DispatchAsync(scheduleMessagePriority, async () =>
        {
            await SendAsync(message, asyncMessageAwaitStrategy, cancellationToken);
        });
    }

    /// <exception cref="ArgumentNullException"/>
    public void Unregister<TMessage, TToken>(object recipient, TToken token) where TMessage : class where TToken : IEquatable<TToken>
    {
        ArgumentNullException.ThrowIfNull(recipient);
        ArgumentNullException.ThrowIfNull(token);

        _messenger.Unregister<TMessage, TToken>(recipient, token);
    }

    /// <exception cref="ArgumentNullException"/>
    public void UnregisterAll(object recipient)
    {
        ArgumentNullException.ThrowIfNull(recipient);

        _messenger.UnregisterAll(recipient);
    }

    /// <exception cref="ArgumentNullException"/>
    public void UnregisterAll<TToken>(object recipient, TToken token) where TToken : IEquatable<TToken>
    {
        ArgumentNullException.ThrowIfNull(recipient);
        ArgumentNullException.ThrowIfNull(token);

        _messenger.UnregisterAll(recipient, token);
    }
}
