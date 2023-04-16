namespace Lexicom.Jwt.Exceptions;
public class BearerTokenNotValidException : Exception
{
    public BearerTokenNotValidException(
        string? bearerToken, 
        string? identifier = null, 
        Exception? innerException = null) : base($"The {(string.IsNullOrWhiteSpace(identifier) ? "" : $"'{identifier}'")} bearer token is not valid.", innerException)
    {
        BearerToken = bearerToken ?? "null";
    }

    public string BearerToken { get; }
}
