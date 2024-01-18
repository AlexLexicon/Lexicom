using Microsoft.Extensions.DependencyInjection;

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

        builder.Services.AddLexicomMvvmViewModel<TViewModelService, TViewModelImplementation>(configure);

        return builder;
    }
}
