using System.Windows.Threading;

namespace Lexicom.Wpf.Amenities.Threading.Extensions;
public static class DispatcherOperationStatusExtensions
{
    /// <exception cref="NotImplementedException"/>
    public static OperationStatus ToAbstraction(this DispatcherOperationStatus dispatcherOperationStatus)
    {
        return dispatcherOperationStatus switch
        {
            DispatcherOperationStatus.Aborted => OperationStatus.Aborted,
            DispatcherOperationStatus.Completed => OperationStatus.Completed,
            DispatcherOperationStatus.Executing => OperationStatus.Executing,
            DispatcherOperationStatus.Pending => OperationStatus.Pending,
            _ => throw new NotImplementedException($"The '{nameof(OperationStatus)}' for the '{nameof(DispatcherOperationStatus)}' value '{dispatcherOperationStatus}' is not implemented."),
        };
    }
}
