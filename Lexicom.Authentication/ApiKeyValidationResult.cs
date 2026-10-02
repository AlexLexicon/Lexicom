using System.Security.Claims;

namespace Lexicom.Authentication;

public readonly struct ApiKeyValidationResult
{
    public ApiKeyValidationResult() 
        : this(
              false, 
              Guid.Empty, 
              Array.Empty<Claim>())
    {
    }
    private ApiKeyValidationResult(
        bool isValid, 
        Guid id, 
        IReadOnlyList<Claim> claims)
    {
        IsValid = isValid;
        ApiKeyId = id;
        ApiKeyClaims = claims;
    }

    public bool IsValid { get; }
    //the id of the api key which is used as the 'sub' claim of the authenticated caller
    public Guid ApiKeyId { get; }
    //the claims to attach to the authenticated caller when the api key is valid
    //include 'permission' claims here so they flow through the 'AddPermissions(...)' policies
    public IReadOnlyList<Claim> ApiKeyClaims => field ?? Array.Empty<Claim>();

    public static ApiKeyValidationResult Invalid() => default;

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

        return new ApiKeyValidationResult(isValid: true, id, claims.ToArray());
    }
}
