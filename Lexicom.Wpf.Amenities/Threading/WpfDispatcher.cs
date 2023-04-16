using Lexicom.Wpf.Amenities.Threading;
using Lexicom.Wpf.Amenities.Threading.Extensions;
using System.Windows.Threading;

namespace Lexicom.Wpf.Amenities;
public class WpfDispatcher : IDispatcher
{
    private readonly Dispatcher _dispatcher;

    public WpfDispatcher(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }


    public void VerifyAccess() => _dispatcher.VerifyAccess();
    public void InvokeShutdown() => _dispatcher.InvokeShutdown();
    public void BeginInvokeShutdown(Priority priority) => _dispatcher.BeginInvokeShutdown(priority.ToConcrete());
    public IDispatcherProcessingDisabled DisableProcessing() => new WpfDispatcherProcessingDisabled(_dispatcher.DisableProcessing());

    public IDispatcherOperation BeginInvoke(Delegate method, params object[] args) => new WpfDispatcherOperation(_dispatcher.BeginInvoke(method, args));
    public IDispatcherOperation BeginInvoke(Delegate method, Priority priority, params object[] args) => new WpfDispatcherOperation(_dispatcher.BeginInvoke(method, priority, args));
    public IDispatcherOperation BeginInvoke(Priority priority, Delegate method) => new WpfDispatcherOperation(_dispatcher.BeginInvoke(priority.ToConcrete(), method));
    public IDispatcherOperation BeginInvoke(Priority priority, Delegate method, object arg) => new WpfDispatcherOperation(_dispatcher.BeginInvoke(priority.ToConcrete(), method, arg));
    public IDispatcherOperation BeginInvoke(Priority priority, Delegate method, object arg, params object[] args) => new WpfDispatcherOperation(_dispatcher.BeginInvoke(priority.ToConcrete(), method, arg, args));

    public void Invoke(Action callback) => _dispatcher.Invoke(callback);
    public void Invoke(Action callback, Priority priority) => _dispatcher.Invoke(callback, priority.ToConcrete());
    public void Invoke(Action callback, Priority priority, CancellationToken cancellationToken) => _dispatcher.Invoke(callback, priority.ToConcrete(), cancellationToken);
    public void Invoke(Action callback, Priority priority, CancellationToken cancellationToken, TimeSpan timeout) => _dispatcher.Invoke(callback, priority.ToConcrete(), cancellationToken, timeout);
    public TResult Invoke<TResult>(Func<TResult> callback) => _dispatcher.Invoke(callback);
    public TResult Invoke<TResult>(Func<TResult> callback, Priority priority) => _dispatcher.Invoke(callback, priority.ToConcrete());
    public TResult Invoke<TResult>(Func<TResult> callback, Priority priority, CancellationToken cancellationToken) => _dispatcher.Invoke<TResult>(callback, priority.ToConcrete(), cancellationToken);
    public TResult Invoke<TResult>(Func<TResult> callback, Priority priority, CancellationToken cancellationToken, TimeSpan timeout) => _dispatcher.Invoke<TResult>(callback, priority.ToConcrete(), cancellationToken, timeout);
    public object Invoke(Priority priority, Delegate method) => _dispatcher.Invoke(priority.ToConcrete(), method);
    public object Invoke(Priority priority, Delegate method, object arg) => _dispatcher.Invoke(priority.ToConcrete(), method, arg);
    public object Invoke(Priority priority, Delegate method, object arg, params object[] args) => _dispatcher.Invoke(priority.ToConcrete(), method, arg, args);
    public object Invoke(Priority priority, TimeSpan timeout, Delegate method) => _dispatcher.Invoke(priority.ToConcrete(), timeout, method);
    public object Invoke(Priority priority, TimeSpan timeout, Delegate method, object arg) => _dispatcher.Invoke(priority.ToConcrete(), timeout, method, arg);
    public object Invoke(Priority priority, TimeSpan timeout, Delegate method, object arg, params object[] args) => _dispatcher.Invoke(priority.ToConcrete(), timeout, method, arg, args);
    public object Invoke(Delegate method, params object[] args) => _dispatcher.Invoke(method, args);
    public object Invoke(Delegate method, TimeSpan timeout, params object[] args) => _dispatcher.Invoke(method, timeout, args);
    public object Invoke(Delegate method, Priority priority, params object[] args) => _dispatcher.Invoke(method, priority.ToConcrete(), args);
    public object Invoke(Delegate method, TimeSpan timeout, Priority priority, params object[] args) => _dispatcher.Invoke(method, timeout, priority.ToConcrete(), args);

    public IDispatcherOperation InvokeAsync(Action callback) => new WpfDispatcherOperation(_dispatcher.InvokeAsync(callback));
    public IDispatcherOperation InvokeAsync(Action callback, Priority priority) => new WpfDispatcherOperation(_dispatcher.InvokeAsync(callback, priority.ToConcrete()));
    public IDispatcherOperation InvokeAsync(Action callback, Priority priority, CancellationToken cancellationToken) => new WpfDispatcherOperation(_dispatcher.InvokeAsync(callback, priority.ToConcrete(), cancellationToken));
    public IDispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback) => new WpfDispatcherOperation<TResult>(_dispatcher.InvokeAsync(callback));
    public IDispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, Priority priority) => new WpfDispatcherOperation<TResult>(_dispatcher.InvokeAsync(callback, priority.ToConcrete()));
    public IDispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, Priority priority, CancellationToken cancellationToken) => new WpfDispatcherOperation<TResult>(_dispatcher.InvokeAsync(callback, priority.ToConcrete(), cancellationToken));
}
