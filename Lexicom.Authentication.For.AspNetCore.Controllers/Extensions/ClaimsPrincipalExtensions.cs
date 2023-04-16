using Lexicom.Jwt.Exceptions;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
public static class ClaimsPrincipalExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    /// <exception cref="ClaimNotValidException"/>
    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        ArgumentNullException.ThrowIfNull(claimsPrincipal);

        Claim? claim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Email);

        if (claim is null)
        {
            throw new ClaimDoesNotExistException(nameof(JwtRegisteredClaimNames), nameof(JwtRegisteredClaimNames.Email));
        }

        string email = claim.Value;

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ClaimNotValidException(nameof(JwtRegisteredClaimNames), nameof(JwtRegisteredClaimNames.Email), "The value was empty.");
        }

        return email;
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ClaimDoesNotExistException"/>
    /// <exception cref="ClaimNotValidException"/>
    public static Guid GetId(this ClaimsPrincipal claimsPrincipal)
    {
        ArgumentNullException.ThrowIfNull(claimsPrincipal);

        Claim? claim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sub);

        if (claim is null)
        {
            throw new ClaimDoesNotExistException(nameof(JwtRegisteredClaimNames), nameof(JwtRegisteredClaimNames.Sub));
        }

        string subjectIdString = claim.Value;

        if (!Guid.TryParse(subjectIdString, out Guid subjectId))
        {
            throw new ClaimNotValidException(nameof(JwtRegisteredClaimNames), nameof(JwtRegisteredClaimNames.Sub), "The value was not a guid.");
        }

        return subjectId;
    }
}