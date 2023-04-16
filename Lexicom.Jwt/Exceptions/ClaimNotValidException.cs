namespace Lexicom.Jwt.Exceptions;
public class ClaimNotValidException : Exception
{
    public ClaimNotValidException(
        string? claim, 
        string? reasonForBeingInvalid) : this("", claim, reasonForBeingInvalid)
    {
    }
    public ClaimNotValidException(
        string? claimSourceName, 
        string? claim, 
        string? reasonForBeingInvalid) : base($"The '{(string.IsNullOrWhiteSpace(claimSourceName) ? "" : $"{claimSourceName ?? "null"}.")}{claim ?? "null"}' claim is not valid: {reasonForBeingInvalid ?? "null"}")
    {
        ClaimSourceName = claimSourceName ?? "null";
        Claim = claim ?? "null";
    }

    public string ClaimSourceName { get; }
    public string Claim { get; }
}
