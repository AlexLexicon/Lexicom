namespace Lexicom.Jwt.Exceptions;

public class ClaimDoesNotExistException(string? claimSourceName, string? claimName) : Exception($"The '{$"{claimSourceName ?? "null"}."}{claimName ?? "null"}' claim does not exist.")
{
    public string? ClaimSourceName { get; } = claimSourceName;
    public string? Claim { get; } = claimName;
}
