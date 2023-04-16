namespace Lexicom.ConsoleApp.Tui.Exceptions;
public class TuiOperationAssemblyScanException<TAssemblyScanMarker> : Exception
{
    public TuiOperationAssemblyScanException(Exception? innerException) : base($"Failed while assembly scanning for '{nameof(ITuiOperation)}' from the assembly scan marker '{typeof(TAssemblyScanMarker).FullName}'.", innerException)
    {
    }
}
