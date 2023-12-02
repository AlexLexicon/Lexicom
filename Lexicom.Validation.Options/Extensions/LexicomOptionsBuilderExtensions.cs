using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Validation.Options.Extensions;
public static class LexicomOptionsBuilderExtensions
{
    public static LexicomOptionsBuilder<TOptions> ValidateOnStart<TOptions>(this LexicomOptionsBuilder<TOptions> optionsBuilder) where TOptions : class
    {
        ArgumentNullException.ThrowIfNull(optionsBuilder);

        optionsBuilder.Services.AddSingleton(new ValidateOptionsStartRegistration(typeof(TOptions), optionsBuilder.Name));

        Microsoft.Extensions.DependencyInjection.OptionsBuilderExtensions.ValidateOnStart(optionsBuilder);

        return optionsBuilder;
    }
}
