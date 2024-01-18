using Lexicom.Jwt.Exceptions;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Lexicom.Jwt.Extensions;
public static class StringExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="BearerTokenNotValidException"/>
    public static JsonWebToken ToJsonWebToken(this string bearerToken)
    {
        ArgumentNullException.ThrowIfNull(bearerToken);

        try
        {
            var jwtSecurityTokenHandler = new JsonWebTokenHandler();

            return (JsonWebToken)jwtSecurityTokenHandler.ReadToken(bearerToken);
        }
        catch (Exception e)
        {
            throw new BearerTokenNotValidException(bearerToken, innerException: e);
        }
    }
}
