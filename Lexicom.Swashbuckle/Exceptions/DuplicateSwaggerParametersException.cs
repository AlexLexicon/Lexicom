namespace Lexicom.Swashbuckle.Exceptions;
public class DuplicateSwaggerParametersException : Exception
{
    public DuplicateSwaggerParametersException(string? paramName, string? methodName, Exception? innerException) : base($"The swagger parameter '{paramName ?? "null"}' may only have one default value provided for the '{methodName ?? "null"}' method.", innerException)
    {
    }
}
