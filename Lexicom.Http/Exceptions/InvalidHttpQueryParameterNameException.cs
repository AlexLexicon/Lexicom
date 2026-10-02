namespace Lexicom.Http.Exceptions;

public class InvalidHttpQueryParameterNameException(string? queryStringParameterName) : Exception($"The query string parameter name '{queryStringParameterName ?? "null"}' is not valid.")
{
}
