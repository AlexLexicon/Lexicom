using Lexicom.Supports.Wpf;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddValidation(this IWpfServiceBuilder builder, Action<IValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddLexicomValidation(configure);

        return builder;
    }
}
