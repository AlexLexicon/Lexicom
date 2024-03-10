using Lexicom.DependencyInjection.Hosting;
using Lexicom.Supports.Blazor.WebAssembly;
using Lexicom.Validation.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.For.Blazor.WebAssembly.Extensions;
public static class BlazorWebAssemblyServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyServiceBuilder AddValidation(this IBlazorWebAssemblyServiceBuilder builder, Action<IValidationServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WebAssemblyHostBuilder.Services.AddSingleton<IAfterServiceProviderBuildService, BlazorWebAssemblyValidateOnStartAfterServiceProviderBuildService>();

        builder.WebAssemblyHostBuilder.Services.AddLexicomValidation(configure);

        return builder;
    }
}
