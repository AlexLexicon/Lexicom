using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Lexicom.Wpf.DependencyInjection;
public sealed class WpfApplicationBuilder
{
    private readonly IHostBuilder _hostBuilder;

    /// <exception cref="ArgumentNullException"/>
    internal WpfApplicationBuilder(Application appxaml)
    {
        ArgumentNullException.ThrowIfNull(appxaml);

        Services = new ServiceCollection();
        Configuration = new ConfigurationManager();

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

    public WpfApplication Build()
    {
        IHost host = _hostBuilder.Build();

        return new WpfApplication(host);
    }
}
