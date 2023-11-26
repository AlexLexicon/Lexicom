namespace Lexicom.Jwt.Exceptions;
public class ClaimDoesNotExistException(string? claimSourceName, string? claim) 
    : Exception($"The bearer token does not have the '{(string.IsNullOrWhiteSpace(claimSourceName) ? "" : $"{claimSourceName ?? "null"}.")}{claim ?? "null"}' claim.")
{
    public ClaimDoesNotExistException(string? claim) : this("", claim)
    {
    }

    public string ClaimSourceName { get; } = claimSourceName ?? "null";
    public string Claim { get; } = claim ?? "null";
}
