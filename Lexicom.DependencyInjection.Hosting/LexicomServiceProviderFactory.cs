using Lexicom.DependencyInjection.Hosting.Exceptions;
using Lexicom.DependencyInjection.Hosting.Extensions;
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

    /// <exception cref="BeforeServiceProviderBuildServiceMissingParameterlessConstructorException"/>
    /// <exception cref="BeforeServiceProviderBuildServiceActivationNullException"/>
    /// <exception cref="BeforeServiceProviderBuildServiceCastException"/>
    /// <exception cref="BeforeServiceProviderBuildServiceImplementationFactoryException"/>
    public IServiceProvider CreateServiceProvider(LexicomServiceProviderFactoryContainerBuilder containerBuilder)
    {
        if (Services is null)
        {
            throw new UnreachableException($"The service collection was null but that should never happen since '{nameof(CreateBuilder)}' will always get called first");
        }

        IReadOnlyList<IBeforeServiceProviderBuildService> beforeServiceProviderBuildServices = Services.ResolveBeforeServiceProviderBuildServices();
        foreach (var beforeServiceProviderBuildService in beforeServiceProviderBuildServices)
        {
            beforeServiceProviderBuildService.OnBeforeServiceProviderBuild(Services);
        }

        ServiceProvider provider = Services.BuildServiceProvider();

        IReadOnlyList<IAfterServiceProviderBuildService> afterServiceProviderBuildServices = provider.ResolveAfterServiceProviderBuildServices();
        foreach (IAfterServiceProviderBuildService afterServiceProviderBuildService in afterServiceProviderBuildServices)
        {
            afterServiceProviderBuildService.OnAfterServiceProviderBuild(provider);
        }

        return provider;
    }

    public class LexicomServiceProviderFactoryContainerBuilder
    {
    }
}
