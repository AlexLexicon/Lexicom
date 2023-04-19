using FluentValidation.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.Extensions;
public static class BlazorWebAssemblyValidationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddValidators<TAssemblyScanMarker>(this IBlazorWebAssemblyValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(builder);

        //blazor web assembly applications need validators to be singletons by default
        ValidationServiceBuilderExtensions.AddValidators<TAssemblyScanMarker>(builder, serviceLifetime);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddRuleSets<TAssemblyScanMarker>(this IBlazorWebAssemblyValidationServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        ArgumentNullException.ThrowIfNull(builder);

        ValidationServiceBuilderExtensions.AddRuleSets<TAssemblyScanMarker>(builder, serviceLifetime);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddLanguageManager<TLanguageManager>(this IBlazorWebAssemblyValidationServiceBuilder builder) where TLanguageManager : LanguageManager, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        ValidationServiceBuilderExtensions.AddLanguageManager<TLanguageManager>(builder);

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddLanguageManager(this IBlazorWebAssemblyValidationServiceBuilder builder, LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(languageManager);

        ValidationServiceBuilderExtensions.AddLanguageManager(builder, languageManager);

        return builder;
    }
}
