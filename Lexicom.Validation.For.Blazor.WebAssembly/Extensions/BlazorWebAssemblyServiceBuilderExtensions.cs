using Lexicom.Supports.Blazor.WebAssembly;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddValidation(this IBlazorWebAssemblyServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddValidation(builder, null);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddValidation(this IBlazorWebAssemblyServiceBuilder builder, Action<IValidationServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddLexicomValidation(builder.WebAssemblyHostBuilder.Configuration, configure);

        return builder;
    }
}
