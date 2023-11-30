using Lexicom.DependencyInjection.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Amenities.Extensions;
public static class MvvmServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddMediatR(this IMvvmServiceBuilder builder, Action<MediatRServiceConfiguration> configure)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(configure);

        var mediatRServiceConfiguration = new MediatRServiceConfiguration();

        configure.Invoke(mediatRServiceConfiguration);

        //you have to provide the configure options in order to specify at least one assembly to scan for mediatR objects
        builder.Services.AddMediatR(mediatRServiceConfiguration);

        builder.Services.AddSingleton<IDependencyInjectionHostPreBuildExecutor, MediatRServiceRegistrationPreBuildExecutor>();

        return builder;
    }
}
