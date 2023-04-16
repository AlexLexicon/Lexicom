using Lexicom.Jwt.Exceptions;
using System.IdentityModel.Tokens.Jwt;

namespace Lexicom.Jwt.Extensions;
public static class StringExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="BearerTokenNotValidException"/>
    public static JwtSecurityToken ToJwtSecurityToken(this string bearerToken)
    {
        ArgumentNullException.ThrowIfNull(bearerToken);

        try
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return (JwtSecurityToken)jwtSecurityTokenHandler.ReadToken(bearerToken);
        }
        catch (Exception e)
        {
            throw new BearerTokenNotValidException(bearerToken, innerException: e);
        }
    }
}
