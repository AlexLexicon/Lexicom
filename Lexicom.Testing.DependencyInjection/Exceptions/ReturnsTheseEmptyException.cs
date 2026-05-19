namespace Lexicom.Testing.DependencyInjection.Exceptions;

public class ReturnsTheseEmptyException() : Exception($"The return these {nameof(IEnumerable<>)} is empty.")
{
}
