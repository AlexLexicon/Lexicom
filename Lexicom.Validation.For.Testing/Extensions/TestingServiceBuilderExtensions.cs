using Lexicom.Supports.Testing;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.Testing.Extensions;

public static class TestingServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static ITestingServiceBuilder AddValidation(this ITestingServiceBuilder builder, Action<IValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomValidation(configure);

        return builder;
    }
}
