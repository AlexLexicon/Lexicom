using Lexicom.DependencyInjection.Primitives.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Primitives.Extensions;
public static class DependencyInjectionPrimitivesServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddTimeProvider(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton(sp =>
        {
            return new TimeProviderInterfaceWrapper(TimeProvider.System);
        });
        builder.Services.AddSingleton<ITimeProvider>(sp =>
        {
            return sp.GetRequiredService<TimeProviderInterfaceWrapper>();
        });

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddGuidProvider(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IGuidProvider, GuidProvider>();

        return builder;
    }
    
    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddRandomProvider(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IRandomProvider, RandomProvider>();

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddRandomProvider(this IDependencyInjectionPrimitivesServiceBuilder builder, int seed)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton<IRandomProvider>(sp =>
        {
            return new RandomProvider(seed);
        });

        return builder;
    }
}