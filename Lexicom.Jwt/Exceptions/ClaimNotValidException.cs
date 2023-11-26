namespace Lexicom.Jwt.Exceptions;
public class ClaimNotValidException(string? claimSourceName, string? claim, string? reasonForBeingInvalid) 
    : Exception($"The '{(string.IsNullOrWhiteSpace(claimSourceName) ? "" : $"{claimSourceName ?? "null"}.")}{claim ?? "null"}' claim is not valid: {reasonForBeingInvalid ?? "null"}")
{
    public ClaimNotValidException(string? claim, string? reasonForBeingInvalid) 
        : this("", claim, reasonForBeingInvalid)
    {
    }

    public string ClaimSourceName { get; } = claimSourceName ?? "null";
    public string Claim { get; } = claim ?? "null";
}
