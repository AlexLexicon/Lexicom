namespace Lexicom.Mvvm.Exceptions;
public class ImplementationTypeDoesNotImplementForwardingTypeException(Type? implementationType, Type? forwardType) : Exception($"The implementation type '{implementationType?.Name ?? "null"}' does not implement the forwarded type '{forwardType?.Name ?? "null"}'.")
{
}
