namespace Lexicom.Mvvm.For.Blazor.WebAssembly;

public class BlazorMessengerScheduler : IMessengerScheduler
{
    public Task DispatchAsync(ScheduleMessagePriority priority, Func<Task>? dispatchedDelegate)
    {
        throw new NotSupportedException($"Scheduling messages is not supported in Blazor web assembly. To ignore this error you can create your own implementation of '{nameof(IMessengerScheduler)}' and replace this implementation in the service collection.");
    }
}
