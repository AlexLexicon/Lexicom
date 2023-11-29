using Lexicom.Mvvm.Support;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Mvvm.Extensions;
public static class MvvmViewModelsServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmViewModelsServiceBuilder AddViewModel<TViewModel>(this IMvvmViewModelsServiceBuilder builder, ServiceLifetime serviceLifetime) where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddViewModel<TViewModel, TViewModel>(builder, serviceLifetime);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmViewModelsServiceBuilder AddViewModel<TViewModelService, TViewModelImplementation>(this IMvvmViewModelsServiceBuilder builder, ServiceLifetime serviceLifetime) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddViewModel<TViewModelService, TViewModelImplementation>(builder, options =>
        {
            options.ServiceLifetime = serviceLifetime;
        });
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmViewModelsServiceBuilder AddViewModel<TViewModel>(this IMvvmViewModelsServiceBuilder builder, Action<IViewModelServiceBuilder>? configure = null) where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddViewModel<TViewModel, TViewModel>(builder, configure);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmViewModelsServiceBuilder AddViewModel<TViewModelService, TViewModelImplementation>(this IMvvmViewModelsServiceBuilder builder, Action<IViewModelServiceBuilder>? configure = null) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(builder);

        var vmbuilder = new ViewModelServiceBuilder(builder.Builder.Services, typeof(TViewModelService), typeof(TViewModelImplementation))
        {
            ServiceLifetime = ServiceLifetime.Singleton
        };

        configure?.Invoke(vmbuilder);

        builder.Builder.AddDeferredRegistration(() =>
        {
            DeferredAddViewModel<TViewModelService, TViewModelImplementation>(vmbuilder);
        }, 0);

        return builder;
    }

    private static void DeferredAddViewModel<TViewModelService, TViewModelImplementation>(ViewModelServiceBuilder builder) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        builder.Services.TryAddSingleton<IViewModelFactory, ViewModelFactory>();

        Type implementationType = typeof(TViewModelImplementation);

        builder.Services.Add(new ServiceDescriptor(implementationType, sp =>
        {
            var viewModelFactory = sp.GetRequiredService<IViewModelFactory>();

            return viewModelFactory.Create<TViewModelImplementation>();
        }, builder.ServiceLifetime));

        Type serviceType = typeof(TViewModelService);
        if (implementationType != serviceType)
        {
            builder.Services.AddSingleton(new ViewModelImplementationTypeAccessor<TViewModelService>
            {
                ViewModelImplementationType = implementationType,
            });
            builder.Services.Add(new ServiceDescriptor(serviceType, sp =>
            {
                return sp.GetRequiredService<TViewModelImplementation>();
            }, builder.ServiceLifetime));
        }

        builder.Services.AddSingleton<WeakViewModelRefrenceCollection<TViewModelImplementation>>();

        builder.Services.AddSingleton(new ViewModelRegistration
        {
            ServiceLifetime = builder.ServiceLifetime,
            ImplementationType = implementationType,
            ServiceType = serviceType,
        });
    }
}
