using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicom.Validation.Options.Extensions;
public static class OptionsBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static OptionsBuilder<T> Validate<T, TValidator>(this OptionsBuilder<T> optionsBuilder) where T : class where TValidator : AbstractOptionsValidator<T>
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);

        optionsBuilder.Services.AddSingleton<IValidator<T>, TValidator>();

        return optionsBuilder.Validate();
    }
    /// <exception cref="ArgumentNullException"/>
    public static OptionsBuilder<T> Validate<T>(this OptionsBuilder<T> optionsBuilder) where T : class
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);

        optionsBuilder.Services.AddSingleton(new LexicomValidateOptions
        {
            OptionsType = typeof(T),
        });
        optionsBuilder.Services.AddSingleton<IValidateOptions<T>>(sp =>
        {
            var validator = sp.GetRequiredService<IValidator<T>>();

            return new FluentValidationValidateOptions<T>(optionsBuilder.Name, validator);
        });

        return optionsBuilder;
    }
}
