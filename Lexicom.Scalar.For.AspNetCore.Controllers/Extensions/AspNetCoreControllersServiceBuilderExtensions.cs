using Lexicom.Supports.AspNetCore.Controllers;

namespace Lexicom.Scalar.Extensions;

public static class AspNetCoreControllersServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddScalar(this IAspNetCoreControllersServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomScalar();

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IAspNetCoreControllersServiceBuilder AddScalar(this IAspNetCoreControllersServiceBuilder builder, Dictionary<string, object?>? defaultParameters)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomScalar(defaultParameters);

        return builder;
    }
}
