using Lexicom.Supports.Blazor.WebAssembly;
using Lexicom.Validation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddValidation(this IBlazorWebAssemblyServiceBuilder builder, Action<IBlazorWebAssemblyValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddSingleton<ILexicomBlazorWebAssemblyBuildService, ValidateOnStartBlazorWebAssemblyBuildService>();

        builder.WebAssemblyHostBuilder.Services.AddLexicomValidation(sb =>
        {
            configure?.Invoke(new BlazorWebAssemblyValidationServiceBuilder(sb.Services, sb.LanguageManager));
        });

        return builder;
    }
}
