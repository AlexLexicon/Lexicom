using Lexicom.DependencyInjection.Hosting.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Lexicom.DependencyInjection.Hosting;
/*
 * we sometimes want to be able to have some processing happen before or just after the service provider is created.
 * to do this you can register either a 'IBeforeServiceProviderBuildService' or a 'IAfterServiceProviderBuildService' service
 * and this provider factory will make sure they are called before and after respectively
 */
public class LexicomServiceProviderFactory : IServiceProviderFactory<LexicomServiceProviderFactory.LexicomServiceProviderFactoryContainerBuilder>
{
    private IServiceCollection? Services { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public LexicomServiceProviderFactoryContainerBuilder CreateBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;

        return new LexicomServiceProviderFactoryContainerBuilder();
    }

    /// <exception cref="PreBuildExecutorMissingParameterlessConstructorException"/>
    /// <exception cref="PreBuildExecutorImplementationFactoryException"/>
    public IServiceProvider CreateServiceProvider(LexicomServiceProviderFactoryContainerBuilder containerBuilder)
    {
        if (Services is null)
        {
            throw new UnreachableException($"The service collection was null but that should never happen since '{nameof(CreateBuilder)}' will always get called first");
        }

        List<ServiceDescriptor> beforeServiceProviderBuildServiceDescriptors = Services
            .Where(sd => sd.ServiceType == typeof(IBeforeServiceProviderBuildService))
            .ToList();

        foreach (ServiceDescriptor beforeServiceProviderBuildServiceDescriptor in beforeServiceProviderBuildServiceDescriptors)
        {
            if (!InvokeWhenInstanceIsBeforeServiceProviderBuildService(beforeServiceProviderBuildServiceDescriptor.ImplementationInstance, Services))
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
                        throw new PreBuildExecutorMissingParameterlessConstructorException(beforeServiceProviderBuildServiceType, e);
                    }

                    if (!InvokeWhenInstanceIsBeforeServiceProviderBuildService(beforeServiceProviderBuildServiceInstance, Services))
                    {
                        throw new UnreachableException($"The implementation instance of the type '{beforeServiceProviderBuildServiceType.FullName}' did not implement the interface '{nameof(IBeforeServiceProviderBuildService)}' but that shouldn't be possible since we queried only for service descriptors where that is true.");
                    }
                }
                else if (beforeServiceProviderBuildServiceDescriptor.ImplementationFactory is not null)
                {
                    throw new PreBuildExecutorImplementationFactoryException();
                }
            }
        }

        ServiceProvider provider = Services.BuildServiceProvider();

        IEnumerable<IAfterServiceProviderBuildService> afterServiceProviderBuildServices = provider.GetServices<IAfterServiceProviderBuildService>();

        foreach (IAfterServiceProviderBuildService afterServiceProviderBuildService in afterServiceProviderBuildServices)
        {
            afterServiceProviderBuildService.OnAfterServiceProviderBuild(provider);
        }

        return provider;

        bool InvokeWhenInstanceIsBeforeServiceProviderBuildService(object? possibleInstance, IServiceCollection services)
        {
            if (possibleInstance is not null and IBeforeServiceProviderBuildService beforeServiceProviderBuildService)
            {
                beforeServiceProviderBuildService.OnBeforeServiceProviderBuild(services);

                return true;
            }

            return false;
        }
    }

    public class LexicomServiceProviderFactoryContainerBuilder
    {
    }
}
