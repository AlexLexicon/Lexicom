namespace Lexicom.Jwt.Exceptions;
public class ClaimDoesNotExistException : Exception
{
    public ClaimDoesNotExistException(string? claim) : this("", claim)
    {
    }
    public ClaimDoesNotExistException(
        string? claimSourceName, 
        string? claim) : base($"The bearer token does not have the '{(string.IsNullOrWhiteSpace(claimSourceName) ? "" : $"{claimSourceName ?? "null"}.")}{claim ?? "null"}' claim.")
    {
        ClaimSourceName = claimSourceName ?? "null";
        Claim = claim ?? "null";
    }

    public string ClaimSourceName { get; }
    public string Claim { get; }
}
