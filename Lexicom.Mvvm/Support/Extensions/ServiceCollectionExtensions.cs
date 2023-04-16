using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Support.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void AugmentViewModelRegistrations(this IServiceCollection services, IViewModelRegistrationAugmenter augmenter)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(augmenter);

        foreach (ViewModelRegistration viewModelRegistration in services.GetViewModelRegistrations())
        {
            viewModelRegistration.Augment(services, augmenter);
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public static List<ViewModelRegistration> GetViewModelRegistrations(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return services
            .Where(sd => sd.ServiceType == typeof(ViewModelRegistration))
            .Select(sd => sd.ImplementationInstance)
            .Cast<ViewModelRegistration>()
            .ToList();
    }
}
