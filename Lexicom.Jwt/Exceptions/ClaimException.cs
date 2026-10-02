namespace Lexicom.Jwt.Exceptions;

public class ClaimException : Exception
{
    public static string GetClaimSourceAndNameString(string? claimSourceName, string? claimName)
    {
        string actualClaimName = claimName ?? "null";

        if (string.IsNullOrWhiteSpace(claimSourceName))
        {
            return actualClaimName;
        }

        return $"{claimSourceName}.{actualClaimName}";
    }

    public ClaimException(string? message) : base(message)
    {
    }
}
