using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives.Extensions;
public static class DependencyInjectionPrimitivesServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddTimeProvider(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<ITimeProvider>(TimeProvider.System);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddGuidProvider(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IGuidProvider, GuidProvider>();

        return builder;
    }
}