namespace Lexicom.Mvvm;

public interface IMessengerScheduler
{
    Task DispatchAsync(ScheduleMessagePriority priority, Func<Task>? dispatchedDelegate);
}
