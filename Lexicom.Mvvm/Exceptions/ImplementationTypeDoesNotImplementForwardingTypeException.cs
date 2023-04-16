namespace Lexicom.Mvvm.Exceptions;
public class ImplementationTypeDoesNotImplementForwardingTypeException : Exception
{
    public ImplementationTypeDoesNotImplementForwardingTypeException(Type? implementationType, Type? forwardType) : base($"The implementation type '{implementationType?.Name ?? "null"}' does not implement the forwarded type '{forwardType?.Name ?? "null"}'.")
    {
    }
}
