using Lexicom.DependencyInjection.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Lexicom.Wpf.DependencyInjection;
public sealed class WpfApplicationBuilder
{
    private readonly HostBuilder _hostBuilder;

    /// <exception cref="ArgumentNullException"/>
    internal WpfApplicationBuilder(Application appxaml)
    {
        ArgumentNullException.ThrowIfNull(appxaml);

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

        Services.AddSingleton(appxaml);
        Services.AddSingleton(appxaml.Dispatcher);
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

    public WpfApplication Build()
    {
        IHost host = _hostBuilder.Build();

        return new WpfApplication(host, Environment);
    }
}
