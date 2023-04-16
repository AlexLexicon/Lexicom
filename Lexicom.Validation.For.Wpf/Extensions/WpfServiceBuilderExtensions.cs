using Lexicom.Supports.Wpf;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddValidation(this IWpfServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddValidation(builder, null);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddValidation(this IWpfServiceBuilder builder, Action<IValidationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddLexicomValidation(builder.WpfApplicationBuilder.Configuration, configure);

        return builder;
    }
}
