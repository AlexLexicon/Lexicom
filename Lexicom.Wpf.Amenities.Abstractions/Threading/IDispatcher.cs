namespace Lexicom.Wpf.Amenities.Threading;
/*
 * the purpose of this wrapping/abstractions
 * is to allow the view models of a wpf application
 * to access the wpf Dispatcher. 
 * the reason we need these interfaces is that
 * usually the view models does not refrence
 * windows namespaces directly so these can be used
 * without needed a windows specific version of dot net
 */
public interface IDispatcher
{
    void VerifyAccess();
    void InvokeShutdown();
    void BeginInvokeShutdown(Priority priority);
    IDispatcherProcessingDisabled DisableProcessing();

    IDispatcherOperation BeginInvoke(Delegate method, params object[] args);
    IDispatcherOperation BeginInvoke(Delegate method, Priority priority, params object[] args);
    IDispatcherOperation BeginInvoke(Priority priority, Delegate method);
    IDispatcherOperation BeginInvoke(Priority priority, Delegate method, object arg);
    IDispatcherOperation BeginInvoke(Priority priority, Delegate method, object arg, params object[] args);

    void Invoke(Action callback);
    void Invoke(Action callback, Priority priority);
    void Invoke(Action callback, Priority priority, CancellationToken cancellationToken);

    //the actual Dispatcher class violates this analysis 
#pragma warning disable CA1068 // CancellationToken parameters must come last
    void Invoke(Action callback, Priority priority, CancellationToken cancellationToken, TimeSpan timeout);
#pragma warning restore CA1068 // CancellationToken parameters must come last
    TResult Invoke<TResult>(Func<TResult> callback);
    TResult Invoke<TResult>(Func<TResult> callback, Priority priority);
    TResult Invoke<TResult>(Func<TResult> callback, Priority priority, CancellationToken cancellationToken);
#pragma warning disable CA1068 // CancellationToken parameters must come last
    //the actual Dispatcher class violates this analysis 
    TResult Invoke<TResult>(Func<TResult> callback, Priority priority, CancellationToken cancellationToken, TimeSpan timeout);
#pragma warning restore CA1068 // CancellationToken parameters must come last
    object Invoke(Priority priority, Delegate method);
    object Invoke(Priority priority, Delegate method, object arg);
    object Invoke(Priority priority, Delegate method, object arg, params object[] args);
    object Invoke(Priority priority, TimeSpan timeout, Delegate method);
    object Invoke(Priority priority, TimeSpan timeout, Delegate method, object arg);
    object Invoke(Priority priority, TimeSpan timeout, Delegate method, object arg, params object[] args);
    object Invoke(Delegate method, params object[] args);
    object Invoke(Delegate method, TimeSpan timeout, params object[] args);
    object Invoke(Delegate method, Priority priority, params object[] args);
    object Invoke(Delegate method, TimeSpan timeout, Priority priority, params object[] args);

    IDispatcherOperation InvokeAsync(Action callback);
    IDispatcherOperation InvokeAsync(Action callback, Priority priority);
    IDispatcherOperation InvokeAsync(Action callback, Priority priority, CancellationToken cancellationToken);
    IDispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback);
    IDispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, Priority priority);
    IDispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, Priority priority, CancellationToken cancellationToken);
}
