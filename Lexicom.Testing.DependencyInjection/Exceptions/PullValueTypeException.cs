namespace Lexicom.Testing.DependencyInjection.Exceptions;

public class PullValueTypeException(Type? type) : Exception($"Cannot pull an instance of the type '{type?.Name ?? "null"}' because it is a value type.")
{
}
