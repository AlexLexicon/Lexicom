using Lexicom.DependencyInjection.Hosting.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Lexicom.DependencyInjection.Hosting;
/*
 * for blazor
 * 
 * the blazor host does not support ValidateOnStart() for options validator
 * so in order to implement that we have to find a round about way of attaching
 * some execution as soon as the blazor app 'Build()' is called
 * to do this I am providing a custom 'IServiceProviderFactory' to the host
 * which really just wraps the default one but we can use the 'CreateServiceProvider' method
 * to do the validation and other startup processes we might add in the future using the 'IDependencyInjectionHostPostBuildService' interface
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

        List<ServiceDescriptor> preBuildExecutors = Services
            .Where(sd => sd.ServiceType == typeof(IDependencyInjectionHostPreBuildService))
            .ToList();

        foreach (ServiceDescriptor preBuildExecutor in preBuildExecutors)
        {
            if (!TryExecute(preBuildExecutor.ImplementationInstance, Services))
            {
                if (preBuildExecutor.ImplementationType is not null)
                {
                    object? implementationInstance;
                    try
                    {
                        implementationInstance = Activator.CreateInstance(preBuildExecutor.ImplementationType);
                    }
                    catch (MissingMethodException e)
                    {
                        throw new PreBuildExecutorMissingParameterlessConstructorException(preBuildExecutor.ImplementationType, e);
                    }

                    if (!TryExecute(implementationInstance, Services))
                    {
                        throw new UnreachableException($"The implementation instance of the type '{preBuildExecutor.ImplementationType.FullName}' did not implement the interface '{nameof(IDependencyInjectionHostPreBuildService)}' but that shouldn't be possible since we queried only for service descriptors where that is true.");
                    }
                }
                else if (preBuildExecutor.ImplementationFactory is not null)
                {
                    throw new PreBuildExecutorImplementationFactoryException();
                }
            }
        }

        ServiceProvider provider = Services.BuildServiceProvider();

        IEnumerable<IDependencyInjectionHostPostBuildService> postBuildServices = provider.GetServices<IDependencyInjectionHostPostBuildService>();

        foreach (IDependencyInjectionHostPostBuildService postBuildService in postBuildServices)
        {
            postBuildService.PostServiceProviderBuilt(provider);
        }

        return provider;

        bool TryExecute(object? implementationInstance, IServiceCollection services)
        {
            if (implementationInstance is not null and IDependencyInjectionHostPreBuildService implementationExecutor)
            {
                implementationExecutor.PreServiceProviderBuilt(services);

                return true;
            }

            return false;
        }
    }

    public class LexicomServiceProviderFactoryContainerBuilder
    {
    }
}
