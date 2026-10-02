namespace Lexicom.Mvvm.For.Testing;

public class TestMessengerScheduler : IMessengerScheduler
{
    public async Task DispatchAsync(ScheduleMessagePriority priority, Func<Task>? dispatchedDelegate)
    {
        if (dispatchedDelegate is not null)
        {
            await dispatchedDelegate.Invoke();
        }
    }
}
