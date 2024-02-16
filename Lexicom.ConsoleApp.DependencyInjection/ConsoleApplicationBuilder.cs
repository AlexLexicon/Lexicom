using Lexicom.DependencyInjection.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lexicom.ConsoleApp.DependencyInjection;
public sealed class ConsoleApplicationBuilder
{
    private readonly HostBuilder _hostBuilder;

    internal ConsoleApplicationBuilder()
    {
        Services = new ServiceCollection();
        Configuration = new ConfigurationManager();

        Environment = LexicomHostApplicationBuilder.InitalizeDefaultConfigurationProvidersAndEnviornment(Configuration);

        _hostBuilder = new HostBuilder();
        _hostBuilder.ConfigureServices(services =>
        {
            Services.AddSingleton<IConfiguration>(Configuration);
            foreach (ServiceDescriptor serviceDescriptor in Services)
            {
                services.Add(serviceDescriptor);
            }
        });
    }

    public IServiceCollection Services { get; }
    public ConfigurationManager Configuration { get; }
    public IHostEnvironment Environment { get; }

    /// <exception cref="ArgumentNullException"/>
    public void ConfigureContainer<TBuilder>(IServiceProviderFactory<TBuilder> factory) where TBuilder : notnull
    {
        ArgumentNullException.ThrowIfNull(factory);

        _hostBuilder.UseServiceProviderFactory(factory);
    }

    public ConsoleApplication Build()
    {
        IHost host = _hostBuilder.Build();

        return new ConsoleApplication(host, Environment);
    }
}
