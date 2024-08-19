using Lexicom.Mvvm.Extensions;
using Lexicom.Mvvm.Support.Extensions;
using Lexicom.Supports.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Lexicom.Mvvm.For.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddMvvm(this IWpfServiceBuilder builder, Action<IMvvmServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomMvvm(configure);

        builder.Services.Replace(new ServiceDescriptor(typeof(IViewModelFactory), typeof(WpfViewModelFactory), ServiceLifetime.Singleton));

        builder.Services.AugmentViewModelRegistrations(new WpfViewModelRegistrationAugmenter());

        return builder;
    }
}
