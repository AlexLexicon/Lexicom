using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Extensions;

public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomMvvm(this IServiceCollection services, Action<IMvvmServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var builder = new MvvmServiceBuilder(services);

        builder.Services.AddSingleton<IViewModelFactory, ViewModelFactory>();
        builder.Services.AddSingleton<WeakReferenceMessenger>(WeakReferenceMessenger.Default);
        builder.Services.AddSingleton<IMessenger, AsyncMessenger>();

        configure?.Invoke(builder);

        return services;
    }
}
