using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomMvvm(this IServiceCollection services, Action<IMvvmServiceBuilder>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var builder = new MvvmServiceBuilder(services);

        configure?.Invoke(builder);

        builder.InvokeDeferredRegistrations();

        return services;
    }
}
