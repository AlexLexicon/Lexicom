using Lexicom.Mvvm.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Mvvm.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomMvvm(this IServiceCollection services, Action<IMvvmServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        configure?.Invoke(new MvvmServiceBuilder(services));

        return services;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomMvvmViewModel<TViewModel>(this IServiceCollection services, Action<IViewModelServiceBuilder>? configure = null) where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(services);

        return AddLexicomMvvmViewModel<TViewModel, TViewModel>(services, configure);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomMvvmViewModel<TViewModelService, TViewModelImplementation>(this IServiceCollection services, Action<IViewModelServiceBuilder>? configure = null) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(services);

        var builder = new ViewModelServiceBuilder(services, typeof(TViewModelService), typeof(TViewModelImplementation))
        {
            ServiceLifetime = ServiceLifetime.Singleton
        };

        configure?.Invoke(builder);

        services.TryAddSingleton<IViewModelFactory, ViewModelFactory>();

        Type implementationType = typeof(TViewModelImplementation);

        services.Add(new ServiceDescriptor(implementationType, sp =>
        {
            var viewModelFactory = sp.GetRequiredService<IViewModelFactory>();

            return viewModelFactory.Create<TViewModelImplementation>();
        }, builder.ServiceLifetime));

        Type serviceType = typeof(TViewModelService);
        if (implementationType != serviceType)
        {
            services.AddSingleton(new ViewModelImplementationTypeAccessor<TViewModelService>
            {
                ViewModelImplementationType = implementationType,
            });
            services.Add(new ServiceDescriptor(serviceType, sp =>
            {
                return sp.GetRequiredService<TViewModelImplementation>();
            }, builder.ServiceLifetime));
        }

        services.AddSingleton<WeakViewModelRefrenceCollection<TViewModelImplementation>>();

        services.AddSingleton(new ViewModelRegistration
        {
            ServiceLifetime = builder.ServiceLifetime,
            ImplementationType = implementationType,
            ServiceType = serviceType,
        });

        return services;
    }
}
