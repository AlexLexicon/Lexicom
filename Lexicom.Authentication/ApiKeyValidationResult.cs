using System.Security.Claims;

namespace Lexicom.Authentication;

public sealed class ApiKeyValidationResult
{
    private static readonly IReadOnlyList<Claim> EmptyClaims = [];

    private ApiKeyValidationResult(bool isValid, Guid id, IReadOnlyList<Claim> claims)
    {
        IsValid = isValid;
        Id = id;
        Claims = claims;
    }

    public bool IsValid { get; }
    //the id of the api key which is used as the 'sub' claim of the authenticated caller
    public Guid Id { get; }
    //the claims to attach to the authenticated caller when the api key is valid
    //include 'permission' claims here so they flow through the 'AddPermissions(...)' policies
    public IReadOnlyList<Claim> Claims { get; }

    public static ApiKeyValidationResult Invalid() => new(false, Guid.Empty, EmptyClaims);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    public static ApiKeyValidationResult Valid(Guid id, params Claim[] claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        return Valid(id, claims.AsEnumerable());
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ArgumentException"/>
    public static ApiKeyValidationResult Valid(Guid id, IEnumerable<Claim> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        if (id == Guid.Empty)
        {
            throw new ArgumentException("The api key id cannot be an empty guid.", nameof(id));
        }

        return new ApiKeyValidationResult(true, id, claims.ToArray());
    }
}
