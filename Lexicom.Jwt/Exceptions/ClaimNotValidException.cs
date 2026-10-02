namespace Lexicom.Jwt.Exceptions;

public class ClaimNotValidException(string? claimSourceName, string? claimName, string? reasonForBeingInvalid) : ClaimException($"The '{GetClaimSourceAndNameString(claimSourceName, claimName)}' claim is not valid: {reasonForBeingInvalid ?? "null"}")
{
    public string ClaimSourceName { get; } = claimSourceName ?? "null";
    public string ClaimName { get; } = claimName ?? "null";
}
