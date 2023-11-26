namespace Lexicom.Jwt.Exceptions;
public class BearerTokenNotValidException(string? bearerToken, string? identifier = null, Exception? innerException = null) 
    : Exception($"The {(string.IsNullOrWhiteSpace(identifier) ? "" : $"'{identifier}'")} bearer token is not valid.", innerException)
{
    public string BearerToken { get; } = bearerToken ?? "null";
}
