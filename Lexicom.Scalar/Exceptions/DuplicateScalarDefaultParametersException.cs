namespace Lexicom.Scalar.Exceptions;

public class DuplicateScalarDefaultParametersException(string? paramName, string? methodName, Exception? innerException) : Exception($"The scalar parameter '{paramName ?? "null"}' may only have one default value provided for the '{methodName ?? "null"}' method.", innerException)
{
}
