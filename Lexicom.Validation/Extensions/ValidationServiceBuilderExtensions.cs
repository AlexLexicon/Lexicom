using FluentValidation.Resources;

namespace Lexicom.Validation.Extensions;
public static class ValidationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddValidators<TAssemblyScanMarker>(this IValidationServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomValidationValidators<TAssemblyScanMarker>();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddRuleSets<TAssemblyScanMarker>(this IValidationServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomValidationRuleSets<TAssemblyScanMarker>();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddLanguageManager<TLanguageManager>(this IValidationServiceBuilder builder) where TLanguageManager : LanguageManager, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddLanguageManager(builder, new TLanguageManager());
    }
    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddLanguageManager(this IValidationServiceBuilder builder, LanguageManager languageManager)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(languageManager);

        builder.LanguageManager = languageManager;

        return builder;
    }
}
