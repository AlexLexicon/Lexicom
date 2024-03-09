using Lexicom.Mvvm.Support;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Mvvm.Extensions;
public static class MvvmServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel<TViewModel>(this IMvvmServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddViewModel<TViewModel, TViewModel>(builder, serviceLifetime);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel<TViewModelService, TViewModelImplementation>(this IMvvmServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddViewModel<TViewModelService, TViewModelImplementation>(builder, options =>
        {
            options.ServiceLifetime = serviceLifetime;
        });
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel<TViewModel>(this IMvvmServiceBuilder builder, Action<IViewModelServiceBuilder> configure) where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        return AddViewModel<TViewModel, TViewModel>(builder, configure);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel<TViewModelService, TViewModelImplementation>(this IMvvmServiceBuilder builder, Action<IViewModelServiceBuilder> configure) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        AddViewModelGeneric<TViewModelService, TViewModelImplementation>(builder, configure);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel(this IMvvmServiceBuilder builder, Type viewModelImplementation, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(viewModelImplementation);

        return AddViewModel(builder, viewModelImplementation, viewModelImplementation, serviceLifetime);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel(this IMvvmServiceBuilder builder, Type viewModelService, Type viewModelImplementation, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(viewModelService);
        ArgumentNullException.ThrowIfNull(viewModelImplementation);

        return AddViewModel(builder, viewModelService, viewModelImplementation, options =>
        {
            options.ServiceLifetime = serviceLifetime;
        });
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel(this IMvvmServiceBuilder builder, Type viewModelImplementation, Action<IViewModelServiceBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);
        ArgumentNullException.ThrowIfNull(viewModelImplementation);

        return AddViewModel(builder, viewModelImplementation, viewModelImplementation, configure);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel(this IMvvmServiceBuilder builder, Type viewModelService, Type viewModelImplementation, Action<IViewModelServiceBuilder> configure)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);
        ArgumentNullException.ThrowIfNull(viewModelService);
        ArgumentNullException.ThrowIfNull(viewModelImplementation);

        StaticAddViewModelGenericMethodInfo
            .MakeGenericMethod(viewModelService, viewModelImplementation)
            .Invoke(obj: null, parameters:
            [
                builder,
                configure,
            ]);

        return builder;
    }

    private static MethodInfo? _staticAddViewModelGenericMethodInfo;
    private static MethodInfo StaticAddViewModelGenericMethodInfo => _staticAddViewModelGenericMethodInfo ??= (typeof(MvvmServiceBuilderExtensions).GetMethod(nameof(AddViewModelGeneric), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(AddViewModelGeneric)}' was not found."));
    private static IMvvmServiceBuilder AddViewModelGeneric<TViewModelService, TViewModelImplementation>(this IMvvmServiceBuilder builder, Action<IViewModelServiceBuilder> configure) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        var vmbuilder = new ViewModelServiceBuilder(builder.Services, typeof(TViewModelService), typeof(TViewModelImplementation))
        {
            ServiceLifetime = ServiceLifetime.Singleton
        };

        configure?.Invoke(vmbuilder);

        builder.Services.TryAddSingleton<IViewModelFactory, ViewModelFactory>();

        Type implementationType = typeof(TViewModelImplementation);

        builder.Services.Add(new ServiceDescriptor(implementationType, sp =>
        {
            var viewModelFactory = sp.GetRequiredService<IViewModelFactory>();

            return viewModelFactory.Create<TViewModelImplementation>();
        }, vmbuilder.ServiceLifetime));

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
            }, vmbuilder.ServiceLifetime));
        }

        builder.Services.AddSingleton<WeakViewModelRefrenceCollection<TViewModelImplementation>>();

        builder.Services.AddSingleton(new ViewModelRegistration
        {
            ServiceLifetime = vmbuilder.ServiceLifetime,
            ImplementationType = implementationType,
            ServiceType = serviceType,
        });

        return builder;
    }
}
