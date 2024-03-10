using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Hosting;
//classes that implement this interface and are registed in the service collection with this interface as their service type will
//have their 'PreServiceProviderBuilt' function called allowing for final services to be registered before the actual service provider is built
//must have a parameterless constructor
public interface IBeforeServiceProviderBuildService
{
    void OnBeforeServiceProviderBuild(IServiceCollection services);
}
