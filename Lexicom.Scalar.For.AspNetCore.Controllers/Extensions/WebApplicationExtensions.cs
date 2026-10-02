using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

namespace Lexicom.Scalar.Extensions;

public static class WebApplicationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void UseLexicomScalar(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.AddPreferredSecuritySchemes(BearerTokenDefaults.AuthenticationScheme);
            });
        }
    }
}
