using Lexicom.Jwt.Exceptions;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Lexicom.Jwt.Extensions;
public static class JwtSecurityTokenExtensions
{
    private static string CachedLowerClaimTypesRole { get; } = ClaimTypes.Role.ToLowerInvariant();
    private static string CachedLowerLexicomJwtClaimTypesPermission { get; } = LexicomJwtClaimTypes.Permission.ToLowerInvariant();

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimNotValidException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    public static string GetEmail(this JsonWebToken jwtSecurityToken)
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
    public static Guid GetJti(this JsonWebToken jwtSecurityToken)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        return GetGuid(jwtSecurityToken, JwtRegisteredClaimNames.Jti, nameof(JwtRegisteredClaimNames));
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimNotValidException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    public static Guid GetSubjectId(this JsonWebToken jwtSecurityToken)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        return GetGuid(jwtSecurityToken, JwtRegisteredClaimNames.Sub, nameof(JwtRegisteredClaimNames));
    }

    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<string> GetRoles(this JsonWebToken jwtSecurityToken)
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

                return claimType == CachedLowerClaimTypesRole || claimType is "role" or "roles";
            })
            .Select(c => c.Value);

        return roleNames;
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasRole(this JsonWebToken jwtSecurityToken, string roleName)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        return jwtSecurityToken
            .GetRoles()
            .Any(r => r == roleName);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IEnumerable<string> GetPermissions(this JsonWebToken jwtSecurityToken)
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

                return claimType == CachedLowerLexicomJwtClaimTypesPermission;
            })
            .Select(c => c.Value);

        return permissions;
    }

    /// <exception cref="ArgumentNullException"/>
    public static bool HasPermission(this JsonWebToken jwtSecurityToken, string permission)
    {
        ArgumentNullException.ThrowIfNull(jwtSecurityToken);

        return jwtSecurityToken
            .GetPermissions()
            .Any(p => p == permission);
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimNotValidException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    public static Guid GetGuid(this JsonWebToken jwtSecurityToken, string claimType, string claimSourceName = "")
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
