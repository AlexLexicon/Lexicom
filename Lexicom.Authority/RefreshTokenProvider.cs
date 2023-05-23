using Lexicom.Authority.Options;
using Lexicom.Authority.Validators;
using Lexicom.Jwt.Options;
using Lexicom.Jwt.Validators;
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
    private readonly IOptions<AuthorityOptions> _authorityOptions;
    private readonly IOptionsMonitor<JwtOptions> _jwtOptions;

    /// <exception cref="ArgumentNullException"/>
    public RefreshTokenProvider(
        IOptions<AuthorityOptions> authorityOptions,
        IOptionsMonitor<JwtOptions> jwtOptions)
    {
        ArgumentNullException.ThrowIfNull(authorityOptions);
        ArgumentNullException.ThrowIfNull(jwtOptions);

        _authorityOptions = authorityOptions;
        _jwtOptions = jwtOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<BearerToken> CreateRefreshTokenAsync(IEnumerable<Claim> claims)
    {
        ArgumentNullException.ThrowIfNull(claims);

        AuthorityOptions authorityOptions = _authorityOptions.Value;
        AuthorityOptionsValidator.ThrowIfNull(authorityOptions.RefreshTokenValidTimeSpan);

        JwtOptions refreshTokenOptions = _jwtOptions.Get(JwtOptions.REFRESH_TOKEN_SECTION);
        JwtOptionsValidator.ThrowIfNull(refreshTokenOptions.SymmetricSecurityKey);

        return CreateBearerTokenAsync(claims, authorityOptions.RefreshTokenValidTimeSpan.Value, refreshTokenOptions.SymmetricSecurityKey);
    }

    /// <exception cref="ArgumentNullException"/>
    public Task<bool> IsRefreshTokenValidAsync(string bearerToken, bool validateLifetime = true)
    {
        ArgumentNullException.ThrowIfNull(bearerToken);

        JwtOptions refreshTokenOptions = _jwtOptions.Get(JwtOptions.REFRESH_TOKEN_SECTION);
        JwtOptionsValidator.ThrowIfNull(refreshTokenOptions.SymmetricSecurityKey);

        return IsBearerTokenValidAsync(bearerToken, validateLifetime, refreshTokenOptions.SymmetricSecurityKey);
    }
}
