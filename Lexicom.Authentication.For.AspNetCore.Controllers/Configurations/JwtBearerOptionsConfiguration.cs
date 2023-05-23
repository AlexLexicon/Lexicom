using Lexicom.Jwt.Options;
using Lexicom.Jwt.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Lexicom.Authentication.For.AspNetCore.Controllers.Configurations;
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

        if (name is "Bearer")
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

        byte[] symmetricSecurityKeyBytes = Encoding.ASCII.GetBytes(accessTokenOptions.SymmetricSecurityKey);

        var symmetricSecurityKey = new SymmetricSecurityKey(symmetricSecurityKeyBytes);

        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = symmetricSecurityKey,
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
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
