using CommunityToolkit.Mvvm.Messaging;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Mvvm.Extensions;

public static class MessengerExtensions
{
    private const AsyncMessageAwaitStrategy DEFAULT_AWAITSTRATEGY = AsyncMessageAwaitStrategy.ForeachAwait;
    private const ScheduleMessagePriority DEFAULT_PRIORITY = ScheduleMessagePriority.ApplicationIdle;

    private static MethodInfo RegisterMethodInfo => field ??= typeof(MessengerExtensions).GetMethod(nameof(AsyncRegister), BindingFlags.Public | BindingFlags.Static) ?? throw new UnreachableException($"The method '{nameof(AsyncRegister)}' was not found.");

    /// <exception cref="ArgumentNullException"/>
    public static async Task SendAsync<TMessage>(this IMessenger messenger, TMessage message, CancellationToken cancellationToken = default) where TMessage : class
    {
        await SendAsync(messenger, message, DEFAULT_AWAITSTRATEGY, cancellationToken);
    }

    /// <exception cref="ArgumentNullException"/>
    public static async Task SendAsync<TMessage>(this IMessenger messenger, TMessage message, AsyncMessageAwaitStrategy asyncMessageAwaitStrategy, CancellationToken cancellationToken = default) where TMessage : class
    {
        ArgumentNullException.ThrowIfNull(messenger);
        ArgumentNullException.ThrowIfNull(message);

        if (messenger is AsyncMessenger asyncMessenger)
        {
            await asyncMessenger.SendAsync(message, asyncMessageAwaitStrategy, cancellationToken);
        }
        else
        {
            throw new NotSupportedException($"The provided '{nameof(IMessenger)}' ('{messenger?.GetType()?.Name ?? "null"}') must be of the type '{nameof(AsyncMessenger)}' to send an async message.");
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public static async Task ScheduleAsync<TMessage>(this IMessenger messenger, TMessage message, CancellationToken cancellationToken = default) where TMessage : class
    {
        await ScheduleAsync(messenger, message, DEFAULT_PRIORITY, DEFAULT_AWAITSTRATEGY, cancellationToken);
    }
    /// <exception cref="ArgumentNullException"/>
    public static async Task ScheduleAsync<TMessage>(this IMessenger messenger, TMessage message, ScheduleMessagePriority scheduleMessagePriority, CancellationToken cancellationToken = default) where TMessage : class
    {
        await ScheduleAsync(messenger, message, scheduleMessagePriority, DEFAULT_AWAITSTRATEGY, cancellationToken);
    }
    /// <exception cref="ArgumentNullException"/>
    public static async Task ScheduleAsync<TMessage>(this IMessenger messenger, TMessage message, AsyncMessageAwaitStrategy asyncMessageAwaitStrategy, CancellationToken cancellationToken = default) where TMessage : class
    {
        await ScheduleAsync(messenger, message, DEFAULT_PRIORITY, asyncMessageAwaitStrategy, cancellationToken);
    }
    /// <exception cref="ArgumentNullException"/>
    public static async Task ScheduleAsync<TMessage>(this IMessenger messenger, TMessage message, ScheduleMessagePriority scheduleMessagePriority, AsyncMessageAwaitStrategy asyncMessageAwaitStrategy, CancellationToken cancellationToken = default) where TMessage : class
    {
        ArgumentNullException.ThrowIfNull(messenger);
        ArgumentNullException.ThrowIfNull(message);

        if (messenger is AsyncMessenger asyncMessenger)
        {
            await asyncMessenger.ScheduleAsync(message, scheduleMessagePriority, asyncMessageAwaitStrategy, cancellationToken);
        }
        else
        {
            throw new NotSupportedException($"The provided '{nameof(IMessenger)}' ('{messenger?.GetType()?.Name ?? "null"}') must be of the type '{nameof(AsyncMessenger)}' to schedule an async message.");
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public static void AsyncRegister<TMessage>(this IMessenger messenger, IAsyncRecipient<TMessage> recipient) where TMessage : class
    {
        ArgumentNullException.ThrowIfNull(messenger);
        ArgumentNullException.ThrowIfNull(recipient);

        if (messenger is AsyncMessenger asyncMessenger)
        {
            asyncMessenger.AsyncRegister(recipient);
        }
        else
        {
            throw new NotSupportedException($"The provided '{nameof(IMessenger)}' ('{messenger?.GetType()?.Name ?? "null"}') must be of the type '{nameof(AsyncMessenger)}' to register an async recipient.");
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public static void AsyncRegisterAll(this IMessenger messenger, object obj)
    {
        ArgumentNullException.ThrowIfNull(messenger);
        ArgumentNullException.ThrowIfNull(obj);

        IEnumerable<Type> asyncRecipientTypes = obj
            .GetType()
            .GetInterfaces()
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncRecipient<>));

        foreach (Type asyncRecipientType in asyncRecipientTypes)
        {
            Type? messageType = asyncRecipientType
                .GetGenericArguments()
                .FirstOrDefault();

            if (messageType is not null)
            {
                MethodInfo registerMethod = RegisterMethodInfo.MakeGenericMethod(messageType);

                registerMethod.Invoke(null,
                [
                    messenger,
                    obj,
                ]);
            }
        }
    }
}
