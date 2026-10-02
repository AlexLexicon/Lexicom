namespace Lexicom.Mvvm.For.Maui.Blazor.Hybrid;

public class MauiMessengerScheduler : IMessengerScheduler
{
    public Task DispatchAsync(ScheduleMessagePriority priority, Func<Task>? dispatchedDelegate)
    {
        throw new NotSupportedException($"Scheduling messages is not supported in Maui Blazor hybrid. To ignore this error you can create your own implementation of '{nameof(IMessengerScheduler)}' and replace this implementation in the service collection.");
    }
}
