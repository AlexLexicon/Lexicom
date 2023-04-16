using Lexicom.Extensions.Exceptions;
using Lexicom.Mvvm.For.Wpf.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Windows;

namespace Lexicom.Mvvm.For.Wpf.Extensions;
public static class ViewModelServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IViewModelServiceBuilder ForWindow<TWindow>(this IViewModelServiceBuilder builder) where TWindow : Window
    {
        ArgumentNullException.ThrowIfNull(builder);

        MethodInfo forWindowGenericMethod = ForWindowMethod.MakeGenericMethod(builder.ServiceType, builder.ImplementationType);

        forWindowGenericMethod.Invoke(null, new object[] 
        { 
            builder, 
            typeof(TWindow) 
        });

        return builder;
    }

    private static MethodInfo? _forWindowMethod;
    private static MethodInfo ForWindowMethod => _forWindowMethod ??= typeof(ViewModelServiceBuilderExtensions).GetMethod(nameof(ForWindowGeneric), BindingFlags.NonPublic | BindingFlags.Static)!;
    private static void ForWindowGeneric<TViewModelService, TViewModelImplementation>(IViewModelServiceBuilder builder, Type windowType) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        builder.Services.AddSingleton<IViewModelWindowCoupler<TViewModelImplementation>>(_ =>
        {
            return new ViewModelWindowCoupler<TViewModelImplementation>(windowType);
        });

        if (typeof(TViewModelService) != typeof(TViewModelImplementation))
        {
            builder.Services.AddSingleton<IViewModelWindowCoupler<TViewModelService>>(_ =>
            {
                return new ViewModelWindowCoupler<TViewModelService>(windowType);
            });
        }

        builder.Services.Add(new ServiceDescriptor(windowType, sp =>
        {
            var wpfViewModelFactory = new WpfViewModelFactory(sp);

            wpfViewModelFactory.CreateViewModelAndTryCoupleWindow<TViewModelImplementation>(out Window? window);

            if (window is null)
            {
                throw new CoupledWindowNullException().ToUnreachableException($"The window was null but that shouldnt be possible because the {nameof(IViewModelWindowCoupler<TViewModelImplementation>)} was registered.");
            }

            return window;
        }, builder.ServiceLifetime));
    }
}
