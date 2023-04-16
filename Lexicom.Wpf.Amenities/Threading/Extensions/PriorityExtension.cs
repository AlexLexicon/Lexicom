using System.Windows.Threading;

namespace Lexicom.Wpf.Amenities.Threading.Extensions;
public static class PriorityExtension
{
    public static DispatcherPriority ToConcrete(this Priority priority)
    {
        return priority switch
        {
            Priority.ApplicationIdle => DispatcherPriority.ApplicationIdle,
            Priority.Background => DispatcherPriority.Background,
            Priority.ContextIdle => DispatcherPriority.ContextIdle,
            Priority.DataBind => DispatcherPriority.DataBind,
            Priority.Inactive => DispatcherPriority.Inactive,
            Priority.Input => DispatcherPriority.Input,
            Priority.Invalid => DispatcherPriority.Invalid,
            Priority.Loaded => DispatcherPriority.Loaded,
            Priority.Normal => DispatcherPriority.Normal,
            Priority.Render => DispatcherPriority.Render,
            Priority.Send => DispatcherPriority.Send,
            Priority.SystemIdle => DispatcherPriority.SystemIdle,
            _ => throw new NotImplementedException($"The '{nameof(DispatcherPriority)}' for the '{nameof(Priority)}' value '{priority}' is not implemented."),
        };
    }
}
