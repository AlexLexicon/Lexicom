using Lexicom.Mvvm.Support;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

namespace Lexicom.Mvvm.Extensions;
public static class MvvmServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel<TViewModel>(this IMvvmServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TViewModel : class
    {
        ArgumentNullException.ThrowIfNull(builder);

        return AddViewModel<TViewModel, TViewModel>(builder, serviceLifetime);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel<TViewModelService, TViewModelImplementation>(this IMvvmServiceBuilder builder, ServiceLifetime serviceLifetime = ServiceLifetime.Transient) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
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
    public static IMvvmServiceBuilder AddViewModel(this IMvvmServiceBuilder builder, Type viewModelImplementation, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(viewModelImplementation);

        return AddViewModel(builder, viewModelImplementation, viewModelImplementation, serviceLifetime);
    }
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddViewModel(this IMvvmServiceBuilder builder, Type viewModelService, Type viewModelImplementation, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
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

    private static MethodInfo StaticAddViewModelGenericMethodInfo => field ??= (typeof(MvvmServiceBuilderExtensions).GetMethod(nameof(AddViewModelGeneric), BindingFlags.Static | BindingFlags.NonPublic) ?? throw new UnreachableException($"The method '{nameof(AddViewModelGeneric)}' was not found."));
    private static IMvvmServiceBuilder AddViewModelGeneric<TViewModelService, TViewModelImplementation>(this IMvvmServiceBuilder builder, Action<IViewModelServiceBuilder> configure) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        var vmbuilder = new ViewModelServiceBuilder(builder.Services, typeof(TViewModelService), typeof(TViewModelImplementation))
        {
            ServiceLifetime = ServiceLifetime.Singleton
        };

        //1. configure this view model
        configure?.Invoke(vmbuilder);

        //2. add the view model factory if it hasnt been added already
        builder.Services.TryAddSingleton<IViewModelFactory, ViewModelFactory>();

        Type implementationType = typeof(TViewModelImplementation);

        //3. register the view model IMPLEMENTATION as a factory pattern that simply calls the IViewModelFactory to create the view model.
        builder.Services.Add(new ServiceDescriptor(implementationType, sp =>
        {
            var viewModelFactory = sp.GetRequiredService<IViewModelFactory>();

            return viewModelFactory.Create<TViewModelImplementation>();
        }, vmbuilder.ServiceLifetime));

        //4. if the view model IMPLEMENTATION and SERVICE types are not the same we need to register the SERVICE type.
        Type serviceType = typeof(TViewModelService);
        if (implementationType != serviceType)
        {
            builder.Services.AddSingleton(new ViewModelImplementationTypeAccessor<TViewModelService>
            {
                ViewModelImplementationType = implementationType,
            });

            //6. register the view model SERVICE type as a factory pattern that simply calls to get the IMPLEMENTATION (see #3).
            builder.Services.Add(new ServiceDescriptor(serviceType, sp =>
            {
                return sp.GetRequiredService<TViewModelImplementation>();
            }, vmbuilder.ServiceLifetime));
        }

        //7. register a weak view model reference collection of this view model IMPLEMENTATION type.
        //this weak reference
        builder.Services.TryAddSingleton<WeakViewModelReferenceCollection<TViewModelImplementation>>();
        builder.Services.TryAddSingleton<IWeakViewModelReferenceCollection<TViewModelImplementation>>(sp =>
        {
            return sp.GetRequiredService<WeakViewModelReferenceCollection<TViewModelImplementation>>();
        });

        builder.Services.AddSingleton(new ViewModelRegistration
        {
            ServiceLifetime = vmbuilder.ServiceLifetime,
            ImplementationType = implementationType,
            ServiceType = serviceType,
        });

        return builder;
    }
}
