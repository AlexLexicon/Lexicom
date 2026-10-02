using Lexicom.Cryptography.Extensions;
using Lexicom.Supports.Testing;

namespace Lexicom.Cryptography.For.Testing.Extensions;

public static class TestingServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ITestingServiceBuilder AddCryptography(this ITestingServiceBuilder builder, Action<ICryptographyServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomCryptography(configure);

        return builder;
    }
}
