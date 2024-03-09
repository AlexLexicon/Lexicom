using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Amenities.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IAssemblyScanBuilder Scan<TAssemblyScanMarker>(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        return new AssemblyScanBuilder(typeof(TAssemblyScanMarker), services);
    }
}
