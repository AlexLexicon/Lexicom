namespace Lexicom.Jwt.Exceptions;

public class ClaimNotValidException(string? claimSourceName, string? claimName, string? reasonForBeingInvalid) 
    : ClaimException(claimSourceName, claimName, $"The '{GetClaimSourceAndNameString(claimSourceName, claimName)}' claim is not valid: {reasonForBeingInvalid ?? "null"}")
{
}
