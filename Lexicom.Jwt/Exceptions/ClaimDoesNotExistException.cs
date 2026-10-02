namespace Lexicom.Jwt.Exceptions;

public class ClaimDoesNotExistException(string? claimSourceName, string? claimName) : ClaimException($"The '{GetClaimSourceAndNameString(claimSourceName, claimName)}' claim does not exist.")
{
    public string? ClaimSourceName { get; } = claimSourceName;
    public string? Claim { get; } = claimName;
}
