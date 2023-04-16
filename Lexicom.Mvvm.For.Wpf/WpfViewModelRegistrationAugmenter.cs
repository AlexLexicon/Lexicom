using Lexicom.Mvvm.Support;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.For.Wpf;
internal class WpfViewModelRegistrationAugmenter : IViewModelRegistrationAugmenter
{
    /// <exception cref="ArgumentNullException"/>
    public void Augment<TViewModelService, TViewModelImplementation>(IServiceCollection services) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddSingleton<ViewModelStartup<TViewModelService>>();
        services.AddSingleton<ViewModelStartup<TViewModelImplementation>>();
    }
}
