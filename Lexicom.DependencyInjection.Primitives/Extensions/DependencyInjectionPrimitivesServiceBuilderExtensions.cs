using Lexicom.DependencyInjection.Primitives.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.DependencyInjection.Primitives.Extensions;
public static class DependencyInjectionPrimitivesServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddTimeProvider(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddSingleton(sp =>
        {
            return TimeProvider.System;
        });
        builder.Services.AddSingleton(sp =>
        {
            var timeProvider = sp.GetRequiredService<TimeProvider>();

            return new TimeProviderInterfaceWrapper(timeProvider);
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
    public static IDependencyInjectionPrimitivesServiceBuilder AddRandomProviderFactory(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.TryAddSingleton<IRandomProviderFactory, RandomProviderFactory>();

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddRandomProvider(this IDependencyInjectionPrimitivesServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        AddRandomProviderFactory(builder);
        builder.Services.AddSingleton(sp =>
        {
            var factory = sp.GetRequiredService<IRandomProviderFactory>();

            return factory.Create();
        });

        return builder;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IDependencyInjectionPrimitivesServiceBuilder AddRandomProvider(this IDependencyInjectionPrimitivesServiceBuilder builder, int seed)
    {
        ArgumentNullException.ThrowIfNull(builder);

        AddRandomProviderFactory(builder);
        builder.Services.AddSingleton(sp =>
        {
            var factory = sp.GetRequiredService<IRandomProviderFactory>();

            return factory.Create(seed);
        });

        return builder;
    }
}