using Lexicom.Authority.Options;
using Lexicom.Authority.Validators;
using Lexicom.Jwt.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Lexicom.Authority;
public interface IAccessTokenProvider
{
    /// <exception cref="ArgumentNullException"/>
    Task<BearerToken> CreateAccessTokenAsync(IEnumerable<Claim> claims);
    /// <exception cref="ArgumentNullException"/>
    Task<bool> IsAccessTokenValidAsync(string bearerToken, bool validateLifetime = true);
}
public class AccessTokenProvider : BearerTokenProvider, IAccessTokenProvider
{
    private readonly JwtOptions _accessTokenOptions;
    private readonly IOptions<AuthorityOptions> _authorityOptions;

    /// <exception cref="ArgumentNullException"/>
    public AccessTokenProvider(
        IOptions<AuthorityOptions> authorityOptions,
        IOptionsMonitor<JwtOptions> jwtOptions)
    {
        ArgumentNullException.ThrowIfNull(authorityOptions);
        ArgumentNullException.ThrowIfNull(jwtOptions);

        _authorityOptions = authorityOptions;
        _accessTokenOptions = jwtOptions.Get(JwtOptions.ACCESS_TOKEN_SECTION);
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<BearerToken> CreateAccessTokenAsync(IEnumerable<Claim> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        AuthorityOptions authorityOptions = _authorityOptions.Value;

        if (authorityOptions.AccessTokenValidTimeSpan is null)
        {
            throw AuthorityOptionsValidator.ToUnreachableException();
        }

        return CreateBearerTokenAsync(claims, authorityOptions.AccessTokenValidTimeSpan.Value, _accessTokenOptions.SymmetricSecurityKey);
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<bool> IsAccessTokenValidAsync(string bearerToken, bool validateLifetime = true)
    {
        ArgumentNullException.ThrowIfNull(bearerToken);

        return IsBearerTokenValidAsync(bearerToken, validateLifetime, _accessTokenOptions.SymmetricSecurityKey);
    }
}
