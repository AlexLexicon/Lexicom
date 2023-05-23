using Lexicom.Authority.Options;
using Lexicom.Authority.Validators;
using Lexicom.Jwt.Options;
using Lexicom.Jwt.Validators;
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
    private readonly IOptions<AuthorityOptions> _authorityOptions;
    private readonly IOptionsMonitor<JwtOptions> _jwtOptions;

    /// <exception cref="ArgumentNullException"/>
    public AccessTokenProvider(
        IOptions<AuthorityOptions> authorityOptions,
        IOptionsMonitor<JwtOptions> jwtOptions)
    {
        ArgumentNullException.ThrowIfNull(authorityOptions);
        ArgumentNullException.ThrowIfNull(jwtOptions);

        _authorityOptions = authorityOptions;
        _jwtOptions = jwtOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<BearerToken> CreateAccessTokenAsync(IEnumerable<Claim> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        AuthorityOptions authorityOptions = _authorityOptions.Value;
        AuthorityOptionsValidator.ThrowIfNull(authorityOptions.AccessTokenValidTimeSpan);

        JwtOptions accessTokenOptions = _jwtOptions.Get(JwtOptions.ACCESS_TOKEN_SECTION);
        JwtOptionsValidator.ThrowIfNull(accessTokenOptions.SymmetricSecurityKey);

        return CreateBearerTokenAsync(claims, authorityOptions.AccessTokenValidTimeSpan.Value, accessTokenOptions.SymmetricSecurityKey);
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<bool> IsAccessTokenValidAsync(string bearerToken, bool validateLifetime = true)
    {
        ArgumentNullException.ThrowIfNull(bearerToken);

        JwtOptions accessTokenOptions = _jwtOptions.Get(JwtOptions.ACCESS_TOKEN_SECTION);
        JwtOptionsValidator.ThrowIfNull(accessTokenOptions.SymmetricSecurityKey);

        return IsBearerTokenValidAsync(bearerToken, validateLifetime, accessTokenOptions.SymmetricSecurityKey);
    }
}
