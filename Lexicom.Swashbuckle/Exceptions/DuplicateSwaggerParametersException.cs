namespace Lexicom.Swashbuckle.Exceptions;
public class DuplicateSwaggerParametersException(string? paramName, string? methodName, Exception? innerException) : Exception($"The swagger parameter '{paramName ?? "null"}' may only have one default value provided for the '{methodName ?? "null"}' method.", innerException)
{
}
