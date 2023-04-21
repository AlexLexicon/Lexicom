using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lexicom.ConsoleApp.DependencyInjection;
public sealed class ConsoleApplication
{
    public static ConsoleApplicationBuilder CreateBuilder()
    {
        return new ConsoleApplicationBuilder();
    }

    private readonly IHost _host;

    /// <exception cref="ArgumentNullException"/>
    internal ConsoleApplication(
        IHost host, 
        IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(host);

        _host = host;

        Configuration = Services.GetRequiredService<IConfiguration>();
        Environment = environment;

        //we start the host here so that
        //validators will run as that is
        //currently the only use for the IHost
        //at this point.
        _host.Start();
    }

    public IConfiguration Configuration { get; }
    public IServiceProvider Services => _host.Services;
    public IHostEnvironment Environment { get; }
}
