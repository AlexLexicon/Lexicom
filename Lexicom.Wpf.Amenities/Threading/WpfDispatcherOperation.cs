using Lexicom.Wpf.Amenities.Threading.Extensions;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Lexicom.Wpf.Amenities.Threading;
public class BaseWpfDispatcherOperation<TDispatcherOperation> : IDispatcherOperation where TDispatcherOperation : DispatcherOperation
{
    public event EventHandler? Aborted;
    public event EventHandler? Completed;

    protected readonly TDispatcherOperation _dispatcherOperation;

    /// <exception cref="ArgumentNullException"/>
    public BaseWpfDispatcherOperation(TDispatcherOperation dispatcherOperation)
    {
        ArgumentNullException.ThrowIfNull(dispatcherOperation);

        _dispatcherOperation = dispatcherOperation;

        _dispatcherOperation.Aborted += DispatcherAborted;
        _dispatcherOperation.Completed += DispatcherCompleted;
    }

    public Task Task => _dispatcherOperation.Task;
    public object Result => _dispatcherOperation.Result;
    public Priority Priority
    {
        get => _dispatcherOperation.Priority.ToAbstraction();
        set => _dispatcherOperation.Priority = value.ToConcrete();
    }
    public IDispatcher Dispatcher => new WpfDispatcher(_dispatcherOperation.Dispatcher);
    public OperationStatus Status => _dispatcherOperation.Status.ToAbstraction();

    public bool Abort() => _dispatcherOperation.Abort();
    public OperationStatus Wait() => _dispatcherOperation.Wait().ToAbstraction();
    public OperationStatus Wait(TimeSpan timeout) => _dispatcherOperation.Wait(timeout).ToAbstraction();
    public TaskAwaiter GetAwaiter() => _dispatcherOperation.GetAwaiter();

    private void DispatcherAborted(object? sender, EventArgs e) => Aborted?.Invoke(sender, e);
    private void DispatcherCompleted(object? sender, EventArgs e) => Completed?.Invoke(sender, e);
}
/// <exception cref="ArgumentNullException"/>
public class WpfDispatcherOperation(DispatcherOperation dispatcherOperation) : BaseWpfDispatcherOperation<DispatcherOperation>(dispatcherOperation)
{
}
/// <exception cref="ArgumentNullException"/>
public class WpfDispatcherOperation<TResult>(DispatcherOperation<TResult> dispatcherOperation) : BaseWpfDispatcherOperation<DispatcherOperation<TResult>>(dispatcherOperation), IDispatcherOperation<TResult>
{
    TResult IDispatcherOperation<TResult>.Result => _dispatcherOperation.Result;
    Task<TResult> IDispatcherOperation<TResult>.Task => _dispatcherOperation.Task;

    TaskAwaiter<TResult> IDispatcherOperation<TResult>.GetAwaiter() => _dispatcherOperation.GetAwaiter();
}
