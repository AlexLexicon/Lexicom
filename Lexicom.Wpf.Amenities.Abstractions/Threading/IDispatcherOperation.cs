using System.Runtime.CompilerServices;

namespace Lexicom.Wpf.Amenities.Threading;
public interface IDispatcherOperation
{
    event EventHandler? Aborted;
    event EventHandler? Completed;

    Task Task { get; }
    object Result { get; }
    Priority Priority { get; set; }
    IDispatcher Dispatcher { get; }
    OperationStatus Status { get; }

    bool Abort();
    OperationStatus Wait();
    OperationStatus Wait(TimeSpan timeout);
    TaskAwaiter GetAwaiter();
}
public interface IDispatcherOperation<TResult> : IDispatcherOperation
{
    new TResult Result { get; }
    new Task<TResult> Task { get; }

    new TaskAwaiter<TResult> GetAwaiter();
}