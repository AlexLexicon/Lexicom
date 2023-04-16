using Lexicom.Authority.Options;
using Lexicom.Authority.Validators;
using Lexicom.Jwt.Options;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Lexicom.Authority;
public interface IRefreshTokenProvider
{
    /// <exception cref="ArgumentNullException"/>
    Task<BearerToken> CreateRefreshTokenAsync(IEnumerable<Claim> claims);
    /// <exception cref="ArgumentNullException"/>
    Task<bool> IsRefreshTokenValidAsync(string bearerToken, bool validateLifetime = true);
}
public class RefreshTokenProvider : BearerTokenProvider, IRefreshTokenProvider
{
    private readonly JwtOptions _refreshTokenOptions;
    private readonly IOptions<AuthorityOptions> _authorityOptions;

    /// <exception cref="ArgumentNullException"/>
    public RefreshTokenProvider(
        IOptions<AuthorityOptions> authorityOptions,
        IOptionsMonitor<JwtOptions> jwtOptions)
    {
        ArgumentNullException.ThrowIfNull(authorityOptions);
        ArgumentNullException.ThrowIfNull(jwtOptions);

        _authorityOptions = authorityOptions;
        _refreshTokenOptions = jwtOptions.Get(JwtOptions.REFRESH_TOKEN_SECTION);
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<BearerToken> CreateRefreshTokenAsync(IEnumerable<Claim> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        AuthorityOptions authorityOptions = _authorityOptions.Value;

        if (authorityOptions.RefreshTokenValidTimeSpan is null)
        {
            throw AuthorityOptionsValidator.ToUnreachableException();
        }

        return CreateBearerTokenAsync(claims, authorityOptions.RefreshTokenValidTimeSpan.Value, _refreshTokenOptions.SymmetricSecurityKey);
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<bool> IsRefreshTokenValidAsync(string bearerToken, bool validateLifetime = true)
    {
        ArgumentNullException.ThrowIfNull(bearerToken);

        return IsBearerTokenValidAsync(bearerToken, validateLifetime, _refreshTokenOptions.SymmetricSecurityKey);
    }
}
