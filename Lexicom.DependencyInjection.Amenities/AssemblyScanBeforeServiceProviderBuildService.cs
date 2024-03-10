using Lexicom.DependencyInjection.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Amenities;
public class AssemblyScanBeforeServiceProviderBuildService : IBeforeServiceProviderBuildService
{
    public void OnBeforeServiceProviderBuild(IServiceCollection services)
    {
        var assemblyScanServiceDescriptors = services
            .Where(s => s.ServiceType == typeof(AssemblyScan))
            .ToList();

        foreach (ServiceDescriptor? serviceDescriptor in assemblyScanServiceDescriptors)
        {
            if (serviceDescriptor.ImplementationInstance is not null and AssemblyScan assemblyScan)
            {
                assemblyScan.Execute();
            }
        }
    }
}
