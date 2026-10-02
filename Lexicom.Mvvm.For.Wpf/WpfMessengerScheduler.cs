using System.Windows;
using System.Windows.Threading;

namespace Lexicom.Mvvm.For.Wpf;

public class WpfMessengerScheduler : IMessengerScheduler
{
    public Task DispatchAsync(ScheduleMessagePriority priority, Func<Task>? dispatchedDelegate)
    {
        if (dispatchedDelegate is not null)
        {
            DispatcherPriority dispatcherPriority;
            if (priority == ScheduleMessagePriority.ApplicationIdle)
            {
                dispatcherPriority = DispatcherPriority.ApplicationIdle;
            }
            else
            {
                dispatcherPriority = DispatcherPriority.Normal;
            }

            //the delegate is queued on the dispatcher and this method returns immediately without
            //awaiting delivery. the callback is intentionally 'async void' so a faulted delivery is
            //re-raised on the dispatcher and reaches Application.DispatcherUnhandledException,
            //rather than being lost as an unobserved task exception (which is what a BeginInvoke
            //of a Func<Task> would do, since the returned task is never observed).
            Action callback = async () => await dispatchedDelegate.Invoke();

            Application.Current.Dispatcher.BeginInvoke(dispatcherPriority, callback);
        }
        return Task.CompletedTask;
    }
}
