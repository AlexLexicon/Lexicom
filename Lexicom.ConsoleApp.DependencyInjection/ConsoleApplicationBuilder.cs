using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lexicom.ConsoleApp.DependencyInjection;
public sealed class ConsoleApplicationBuilder
{
    private readonly IHostBuilder _hostBuilder;

    internal ConsoleApplicationBuilder()
    {
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
    }

    public IServiceCollection Services { get; }
    public ConfigurationManager Configuration { get; }

    public ConsoleApplication Build()
    {
        IHost host = _hostBuilder.Build();

        return new ConsoleApplication(host);
    }
}
