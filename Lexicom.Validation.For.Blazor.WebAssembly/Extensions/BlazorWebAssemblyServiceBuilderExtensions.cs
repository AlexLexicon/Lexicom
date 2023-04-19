using Lexicom.Supports.Blazor.WebAssembly;
using Lexicom.Validation.Extensions;

namespace Lexicom.Validation.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddValidation(this IBlazorWebAssemblyServiceBuilder builder, Action<IBlazorWebAssemblyValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddLexicomValidation(sb =>
        {
            configure?.Invoke(new BlazorWebAssemblyValidationServiceBuilder(sb));
        });

        return builder;
    }
}
