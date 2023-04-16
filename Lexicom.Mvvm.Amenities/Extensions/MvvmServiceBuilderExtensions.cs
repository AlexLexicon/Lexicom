using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Mvvm.Amenities.Extensions;
public static class MvvmServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IMvvmServiceBuilder AddMediatR(this IMvvmServiceBuilder builder, Action<MediatRServiceConfiguration>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomMvvmMediatR(configure);

        return builder;
    }
}
