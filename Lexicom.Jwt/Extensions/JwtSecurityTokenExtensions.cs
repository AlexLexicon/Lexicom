using Lexicom.Jwt.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Lexicom.Jwt.Extensions;
public static class JwtSecurityTokenExtensions
{
    private static string CachedLowerClaimTypesRole { get; } = ClaimTypes.Role.ToLowerInvariant();
    private static string CachedLowerLexicomJwtClaimTypesPermission { get; } = LexicomJwtClaimTypes.Permission.ToLowerInvariant();

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimNotValidException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    public static string GetEmail(this JwtSecurityToken jwtSecurityToken)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        Claim? userClaim;
        try
        {
            userClaim = jwtSecurityToken.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);
        }
        catch (InvalidOperationException)
        {
            throw new ClaimNotValidException(nameof(JwtRegisteredClaimNames), nameof(JwtRegisteredClaimNames.Email), "The token had duplicate claims.");
        }

        if (userClaim is null)
        {
            throw new ClaimDoesNotExistException(nameof(JwtRegisteredClaimNames), nameof(JwtRegisteredClaimNames.Email));
        }

        string userEmail = userClaim.Value;

        if (string.IsNullOrWhiteSpace(userEmail))
        {
            throw new ClaimNotValidException(nameof(JwtRegisteredClaimNames), nameof(JwtRegisteredClaimNames.Email), "The value was empty.");
        }

        return userEmail;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimNotValidException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    public static Guid GetJti(this JwtSecurityToken jwtSecurityToken)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        return GetGuid(jwtSecurityToken, JwtRegisteredClaimNames.Jti, nameof(JwtRegisteredClaimNames));
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimNotValidException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    public static Guid GetSubjectId(this JwtSecurityToken jwtSecurityToken)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        return GetGuid(jwtSecurityToken, JwtRegisteredClaimNames.Sub, nameof(JwtRegisteredClaimNames));
    }

    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<string> GetRoles(this JwtSecurityToken jwtSecurityToken)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        IEnumerable<string> roleNames = jwtSecurityToken.Claims
            .Where(c =>
            {
                if (string.IsNullOrWhiteSpace(c.Value))
                {
                    return false;
                }

                string claimType = c.Type.ToLowerInvariant();

                if (claimType != CachedLowerClaimTypesRole)
                {
                    return false;
                }

                if (claimType is not "role" or "roles")
                {
                    return false;
                }

                return true;
            })
            .Select(c => c.Value);

        return roleNames;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<string> GetPermissions(this JwtSecurityToken jwtSecurityToken)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        IEnumerable<string> permissions = jwtSecurityToken.Claims
            .Where(c =>
            {
                if (string.IsNullOrWhiteSpace(c.Value))
                {
                    return false;
                }

                string claimType = c.Type.ToLowerInvariant();

                if (claimType != CachedLowerLexicomJwtClaimTypesPermission)
                {
                    return false;
                }

                return true;
            })
            .Select(c => c.Value);

        return permissions;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimNotValidException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    public static Guid GetGuid(this JwtSecurityToken jwtSecurityToken, string claimType, string claimSourceName = "")
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);
        ArgumentNullException.ThrowIfNull(claimType);
        ArgumentNullException.ThrowIfNull(claimSourceName);

        Claim? claim;
        try
        {
            claim = jwtSecurityToken.Claims.SingleOrDefault(c => c.Type == claimType);
        }
        catch (InvalidOperationException)
        {
            throw new ClaimNotValidException(claimSourceName, claimType, "The token had duplicate claims.");
        }

        if (claim is null)
        {
            throw new ClaimDoesNotExistException(claimSourceName, claimType);
        }

        if (!Guid.TryParse(claim.Value, out Guid claimGuid))
        {
            throw new ClaimNotValidException(claimSourceName, claimType, "The value was not a guid.");
        }

        return claimGuid;
    }
}
