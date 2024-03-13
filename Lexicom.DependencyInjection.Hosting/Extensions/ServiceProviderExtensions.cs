using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Hosting.Extensions;
public static class ServiceProviderExtensions
{
    public static IReadOnlyList<IAfterServiceProviderBuildService> ResolveAfterServiceProviderBuildServices(this IServiceProvider provider)
    {
        IEnumerable<IAfterServiceProviderBuildService> afterServiceProviderBuildServices = provider.GetServices<IAfterServiceProviderBuildService>();

        return afterServiceProviderBuildServices
            .OrderBy(aspbs => aspbs.Priority)
            .ToList();
    }
}
