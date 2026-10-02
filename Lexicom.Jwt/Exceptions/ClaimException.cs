namespace Lexicom.Jwt.Exceptions;

public class ClaimException(string? claimSourceName, string? claimName, string? message) : Exception(message)
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

    public string ClaimSourceName { get; } = claimSourceName ?? "null";
    public string ClaimName { get; } = claimName ?? "null";

    public string GetClaimSourceAndNameString() => GetClaimSourceAndNameString(claimSourceName, claimName);
}
