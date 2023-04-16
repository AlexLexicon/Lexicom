using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Support;
public interface IViewModelRegistrationAugmenter
{
    /// <exception cref="ArgumentNullException"/>
    void Augment<TViewModelService, TViewModelImplementation>(IServiceCollection services) where TViewModelService : notnull where TViewModelImplementation : class, TViewModelService;
}
