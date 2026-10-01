using System.Security.Claims;

namespace Lexicom.Authentication;

public sealed class ApiKeyValidationResult
{
    private static readonly IReadOnlyList<Claim> EmptyClaims = [];

    private ApiKeyValidationResult(bool isValid, IReadOnlyList<Claim> claims)
    {
        IsValid = isValid;
        Claims = claims;
    }

    public bool IsValid { get; }
    //the claims to attach to the authenticated caller when the api key is valid
    //include 'permission' claims here so they flow through the 'AddPermissions(...)' policies
    public IReadOnlyList<Claim> Claims { get; }

    public static ApiKeyValidationResult Invalid() => new(false, EmptyClaims);

    /// <exception cref="ArgumentNullException"/>
    public static ApiKeyValidationResult Valid(params Claim[] claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        return new ApiKeyValidationResult(true, claims);
    }

    /// <exception cref="ArgumentNullException"/>
    public static ApiKeyValidationResult Valid(IEnumerable<Claim> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        return new ApiKeyValidationResult(true, claims.ToArray());
    }
}
