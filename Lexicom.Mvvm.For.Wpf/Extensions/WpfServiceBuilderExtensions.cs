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

        builder.WpfApplicationBuilder.Services.AddLexicomMvvm(configure);

        builder.WpfApplicationBuilder.Services.Replace(new ServiceDescriptor(typeof(IViewModelFactory), typeof(WpfViewModelFactory), ServiceLifetime.Singleton));

        builder.WpfApplicationBuilder.Services.AugmentViewModelRegistrations(new WpfViewModelRegistrationAugmenter());

        return builder;
    }
}
