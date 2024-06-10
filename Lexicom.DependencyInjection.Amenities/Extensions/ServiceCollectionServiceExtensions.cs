namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionServiceExtensions
{
    /// <summary>
    /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
    /// implementation of the type specified in <paramref name="implementationType"/> to the
    /// specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Transient"/>
    public static IServiceCollection AddTransient<TService>(this IServiceCollection services, Type implementationType) where TService : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(implementationType);

        services.Add(new ServiceDescriptor(typeof(TService), implementationType, ServiceLifetime.Transient));

        return services;
    }

    /// <summary>
    /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
    /// implementation of the type specified in <paramref name="implementationType"/> to the
    /// specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Scoped"/>
    public static IServiceCollection AddScoped<TService>(this IServiceCollection services, Type implementationType) where TService : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(implementationType);

        services.Add(new ServiceDescriptor(typeof(TService), implementationType, ServiceLifetime.Scoped));

        return services;
    }

    /// <summary>
    /// Adds a singleton service of the type specified in <typeparamref name="TService"/> with an
    /// implementation of the type specified in <paramref name="implementationType"/> to the
    /// specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <typeparam name="TService">The type of the service to add.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
    /// <param name="implementationType">The implementation type of the service.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    /// <seealso cref="ServiceLifetime.Singleton"/>
    public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, Type implementationType) where TService : class
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(implementationType);

        services.Add(new ServiceDescriptor(typeof(TService), implementationType, ServiceLifetime.Singleton));

        return services;
    }
}
