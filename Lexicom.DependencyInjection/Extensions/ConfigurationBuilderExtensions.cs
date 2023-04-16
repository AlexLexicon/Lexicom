using Microsoft.Extensions.Configuration;

namespace Lexicom.DependencyInjection.Extensions;
public static class ConfigurationBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConfigurationBuilder AddInMemoryCollection<TConfigurationInMemorySource>(this IConfigurationBuilder builder) where TConfigurationInMemorySource : ConfigurationInMemorySource, new()
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddInMemoryCollection(builder, new TConfigurationInMemorySource());
    }
    /// <exception cref="ArgumentNullException"/>
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder builder, params object?[] configurations)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configurations);

        var configurationInMemorySource = new ConfigurationInMemorySource();

        foreach (object? configuration in configurations)
        {
            configurationInMemorySource.Add(configuration);
        }

        return builder.AddInMemoryCollection(configurationInMemorySource);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder builder, ConfigurationInMemorySource inMemorySource)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(inMemorySource);

        return builder.AddInMemoryCollection(initialData: inMemorySource);
    }
}
