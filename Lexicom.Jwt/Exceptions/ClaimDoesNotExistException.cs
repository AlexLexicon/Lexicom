namespace Lexicom.Jwt.Exceptions;

public class ClaimDoesNotExistException(string? claimSourceName, string? claimName) 
    : ClaimException(claimSourceName, claimName, $"The '{GetClaimSourceAndNameString(claimSourceName, claimName)}' claim does not exist.")
{
}
