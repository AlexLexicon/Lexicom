using FluentValidation.Resources;
using Lexicom.Validation.For.Blazor.WebAssembly;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.Extensions;
public static class BlazorWebAssemblyValidationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddValidators<TAssemblyScanMarker>(this IBlazorWebAssemblyValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(builder);

        //blazor web assembly applications need validators to be singletons by default
        builder.ValidationBuilder.AddValidators<TAssemblyScanMarker>(serviceLifetime);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddRuleSets<TAssemblyScanMarker>(this IBlazorWebAssemblyValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ValidationBuilder.AddRuleSets<TAssemblyScanMarker>(serviceLifetime);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddLanguageManager<TLanguageManager>(this IBlazorWebAssemblyValidationServiceBuilder builder) where TLanguageManager : LanguageManager, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ValidationBuilder.AddLanguageManager<TLanguageManager>();

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddLanguageManager(this IBlazorWebAssemblyValidationServiceBuilder builder, LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(languageManager);

        builder.ValidationBuilder.AddLanguageManager(languageManager);

        return builder;
    }
}
