using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace Lexicom.Authentication.For.AspNetCore.Controllers.Configurations;
public class AuthenticationOptionsConfiguration : IConfigureOptions<AuthenticationOptions>
{
    /// <exception cref="ArgumentNullException"/>
    public void Configure(AuthenticationOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);

        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
}
