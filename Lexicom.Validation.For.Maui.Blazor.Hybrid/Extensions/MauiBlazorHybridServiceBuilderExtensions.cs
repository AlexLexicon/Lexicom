using Lexicom.Supports.Maui.Blazor.Hybrid;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.Maui.Blazor.Hybrid.Extensions;
public static class MauiBlazorHybridServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMauiBlazorHybridServiceBuilder AddValidation(this IMauiBlazorHybridServiceBuilder builder, Action<IValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomValidation(configure);

        return builder;
    }
}
