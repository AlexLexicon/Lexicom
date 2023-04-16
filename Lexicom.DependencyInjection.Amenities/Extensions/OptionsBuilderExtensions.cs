using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Lexicom.DependencyInjection.Amenities.Extensions;
public static class OptionsBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static OptionsBuilder<T> BindConfiguration<T>(this OptionsBuilder<T> builder, Action<BinderOptions>? configure = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        return builder.BindConfiguration(typeof(T).Name, configure);
    }
}
