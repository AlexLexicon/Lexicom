using Lexicom.Jwt.Options;
using Lexicom.Jwt.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lexicom.Authentication.Configurations;

public class JwtBearerOptionsConfiguration : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IOptionsMonitor<JwtOptions> _jwtOptions;

    /// <exception cref="ArgumentNullException"/>
    public JwtBearerOptionsConfiguration(IOptionsMonitor<JwtOptions> jwtOptions)
    {
        ArgumentNullException.ThrowIfNull(jwtOptions);

        _jwtOptions = jwtOptions;
    }

    /// <exception cref="ArgumentNullException"/>
    public void Configure(string? name, JwtBearerOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        if (name is JwtBearerDefaults.AuthenticationScheme)
        {
            Configure(options);
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public void Configure(JwtBearerOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        JwtOptions accessTokenOptions = _jwtOptions.Get(JwtOptions.ACCESS_TOKEN_SECTION);
        JwtOptionsValidator.ThrowIfNull(accessTokenOptions.SymmetricSecurityKey);

        byte[] symmetricSecurityKeyBytes = Encoding.UTF8.GetBytes(accessTokenOptions.SymmetricSecurityKey);

        var symmetricSecurityKey = new SymmetricSecurityKey(symmetricSecurityKeyBytes);

        options.SaveToken = true;
        options.MapInboundClaims = accessTokenOptions.MapInboundClaims;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = symmetricSecurityKey,
            ValidateIssuer = accessTokenOptions.ValidIssuer is not null,
            ValidIssuer = accessTokenOptions.ValidIssuer,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = accessTokenOptions.ClockSkew,
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = OnMessageReceived,
            OnAuthenticationFailed = OnAuthenticationFailed,
            OnChallenge = OnChallenge,
            OnTokenValidated = OnTokenValidated,
            OnForbidden = OnForbidden,
        };
    }

    protected virtual Task OnMessageReceived(MessageReceivedContext context) => Task.CompletedTask;

    protected virtual Task OnAuthenticationFailed(AuthenticationFailedContext context) => Task.CompletedTask;

    protected virtual Task OnChallenge(JwtBearerChallengeContext context) => Task.CompletedTask;

    protected virtual Task OnTokenValidated(TokenValidatedContext context) => Task.CompletedTask;

    protected virtual Task OnForbidden(ForbiddenContext context) => Task.CompletedTask;
}
