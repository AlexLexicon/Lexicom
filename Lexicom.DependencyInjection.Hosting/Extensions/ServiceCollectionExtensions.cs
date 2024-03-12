using Lexicom.DependencyInjection.Hosting.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Hosting.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="BeforeServiceProviderBuildServiceMissingParameterlessConstructorException"/>
    /// <exception cref="BeforeServiceProviderBuildServiceActivationNullException"/>
    /// <exception cref="BeforeServiceProviderBuildServiceCastException"/>
    /// <exception cref="BeforeServiceProviderBuildServiceImplementationFactoryException"/>
    public static IReadOnlyList<IBeforeServiceProviderBuildService> ResolveBeforeServiceProviderBuildServices(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        List<ServiceDescriptor> beforeServiceProviderBuildServiceDescriptors = services
            .Where(sd => sd.ServiceType == typeof(IBeforeServiceProviderBuildService))
            .ToList();

        var beforeServiceProviderBuildServices = new List<IBeforeServiceProviderBuildService>();
        foreach (ServiceDescriptor beforeServiceProviderBuildServiceDescriptor in beforeServiceProviderBuildServiceDescriptors)
        {
            if (beforeServiceProviderBuildServiceDescriptor.ImplementationInstance is not null and IBeforeServiceProviderBuildService beforeServiceProviderBuildService)
            {
                beforeServiceProviderBuildServices.Add(beforeServiceProviderBuildService);
            }
            else
            {
                Type? beforeServiceProviderBuildServiceType = beforeServiceProviderBuildServiceDescriptor.ImplementationType;
                if (beforeServiceProviderBuildServiceType is not null)
                {
                    object? beforeServiceProviderBuildServiceInstance;
                    try
                    {
                        beforeServiceProviderBuildServiceInstance = Activator.CreateInstance(beforeServiceProviderBuildServiceType);
                    }
                    catch (MissingMethodException e)
                    {
                        throw new BeforeServiceProviderBuildServiceMissingParameterlessConstructorException(beforeServiceProviderBuildServiceType, e);
                    }

                    if (beforeServiceProviderBuildServiceInstance is null)
                    {
                        throw new BeforeServiceProviderBuildServiceActivationNullException(beforeServiceProviderBuildServiceType);
                    }

                    IBeforeServiceProviderBuildService castedBeforeServiceProviderBuildService;
                    try
                    {
                        castedBeforeServiceProviderBuildService = (IBeforeServiceProviderBuildService)beforeServiceProviderBuildServiceInstance;
                    }
                    catch (Exception e)
                    {
                        throw new BeforeServiceProviderBuildServiceCastException(beforeServiceProviderBuildServiceType, e);
                    }

                    beforeServiceProviderBuildServices.Add(castedBeforeServiceProviderBuildService);
                }
                else if (beforeServiceProviderBuildServiceDescriptor.ImplementationFactory is not null)
                {
                    throw new BeforeServiceProviderBuildServiceImplementationFactoryException();
                }
            }
        }

        return beforeServiceProviderBuildServices;
    }
}
