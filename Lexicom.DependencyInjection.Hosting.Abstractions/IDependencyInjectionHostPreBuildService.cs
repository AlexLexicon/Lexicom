using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.DependencyInjection.Hosting;
//classes that implement this interface and are registed in the service collection with this interface as their service type will
//have their execute function called allowing for final services to be registered before the actual service provider is built
public interface IDependencyInjectionHostPreBuildService
{
    void PreServiceProviderBuilt(IServiceCollection services);
}
