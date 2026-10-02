namespace Lexicom.Testing.DependencyInjection.Exceptions;

public class MakeZeroConstructorsException(Type type) : Exception($"The {nameof(UnitTestAssistant)} cannot make an instance from the type '{type?.Name ?? "null"}' because it has zero public constructors but requires exactly one.")
{
}
