namespace Lexicom.Testing.DependencyInjection.Exceptions;

public class MakeTooManyConstructorsException(Type? type, int constructorsCount) : Exception($"The {nameof(UnitTestAssistant)} cannot make an instance from the type '{type?.Name ?? "null"}' because it has '{constructorsCount}' public constructors but requires exactly one.")
{
}
