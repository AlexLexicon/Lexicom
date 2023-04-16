using System.Windows.Threading;

namespace Lexicom.Wpf.Amenities.Threading.Extensions;
public static class DispatcherPriorityExtensions
{
    /// <exception cref="NotImplementedException"/>
    public static Priority ToAbstraction(this DispatcherPriority dispatcherPriority)
    {
        return dispatcherPriority switch
        {
            DispatcherPriority.ApplicationIdle => Priority.ApplicationIdle,
            DispatcherPriority.Background => Priority.Background,
            DispatcherPriority.ContextIdle => Priority.ContextIdle,
            DispatcherPriority.DataBind => Priority.DataBind,
            DispatcherPriority.Inactive => Priority.Inactive,
            DispatcherPriority.Input => Priority.Input,
            DispatcherPriority.Invalid => Priority.Invalid,
            DispatcherPriority.Loaded => Priority.Loaded,
            DispatcherPriority.Normal => Priority.Normal,
            DispatcherPriority.Render => Priority.Render,
            DispatcherPriority.Send => Priority.Send,
            DispatcherPriority.SystemIdle => Priority.SystemIdle,
            _ => throw new NotImplementedException($"The '{nameof(Priority)}' for the '{nameof(DispatcherPriority)}' value '{dispatcherPriority}' is not implemented."),
        };
    }
}
