using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lexicom.Mvvm.Support.Extensions;
public static class ViewModelRegistrationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void Augment(this ViewModelRegistration registration, IServiceCollection services, IViewModelRegistrationAugmenter augmenter)
    {
        ArgumentNullException.ThrowIfNull(registration);
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(augmenter);

        MethodInfo augmentMethod = augmenter
            .GetType()
            .GetMethod(nameof(IViewModelRegistrationAugmenter.Augment))!;

        MethodInfo genericAugmentMethod = augmentMethod.MakeGenericMethod(registration.ServiceType, registration.ImplementationType);

        genericAugmentMethod.Invoke(augmenter, new object[] { services });
    }
}
