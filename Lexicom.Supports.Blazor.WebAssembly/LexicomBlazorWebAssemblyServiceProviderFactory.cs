using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Lexicom.Supports.Blazor.WebAssembly;
//the blazor host does not support ValidateOnStart() for options validator
//so in order to implement that we have to go a round about way of attaching
//some execution as soon as the blazor app 'Build()' is called
//to do this I am providing a custom 'IServiceProviderFactory'
//which really just wraps the default one but we can use the 'CreateServiceProvider' method
//to do the validation and other startup processes we might add in the future
public class LexicomBlazorWebAssemblyServiceProviderFactory : IServiceProviderFactory<LexicomBlazorWebAssemblyServiceProviderFactory.LexicomBlazorWebAssemblyServiceProviderFactoryContainer>
{
    private IServiceCollection? Services { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public LexicomBlazorWebAssemblyServiceProviderFactoryContainer CreateBuilder(IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        Services = services;

        return new LexicomBlazorWebAssemblyServiceProviderFactoryContainer();
    }

    public IServiceProvider CreateServiceProvider(LexicomBlazorWebAssemblyServiceProviderFactoryContainer containerBuilder)
    {
        if (Services is null)
        {
            throw new UnreachableException($"The service collection was null but that should never happen since '{nameof(CreateBuilder)}' will always get called first");
        }

        ServiceProvider provider = Services.BuildServiceProvider();

        var buildServices = provider.GetServices<ILexicomBlazorWebAssemblyBuildService>();

        foreach (ILexicomBlazorWebAssemblyBuildService buildService in buildServices)
        {
            buildService.Execute(provider);
        }

        return provider;
    }

    public class LexicomBlazorWebAssemblyServiceProviderFactoryContainer
    {
    }
}
