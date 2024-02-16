using Lexicom.Wpf.DependencyInjection.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using System.Windows.Threading;

namespace Lexicom.Wpf.DependencyInjection;
public sealed class WpfApplication
{
    /// <exception cref="ArgumentNullException"/>
    public static WpfApplicationBuilder CreateBuilder(Application appxaml)
    {
        ArgumentNullException.ThrowIfNull(appxaml);

        return new WpfApplicationBuilder(appxaml);
    }

    private readonly IHost _host;

    /// <exception cref="ArgumentNullException"/>
    internal WpfApplication(
        IHost host,
        IHostEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(host);
        ArgumentNullException.ThrowIfNull(environment);

        _host = host;

        Configuration = Services.GetRequiredService<IConfiguration>();
        Environment = environment;

        var appxaml = Services.GetRequiredService<Application>();

        appxaml.Startup += StartupAsync;
        appxaml.Exit += ExitAsync;
    }

    public IConfiguration Configuration { get; }
    public IServiceProvider Services => _host.Services;
    public IHostEnvironment Environment { get; }

    private Type? StartupWindowType { get; set; }
    private Type? StartupType { get; set; }

    public void StartupWindow<TWindow>() where TWindow : Window
    {
        if (StartupWindowType is not null)
        {
            throw new StartupTypeAlreadyDefinedException(StartupWindowType);
        }

        if (StartupType is not null)
        {
            throw new StartupTypeAlreadyDefinedException(StartupType);
        }

        StartupWindowType = typeof(TWindow);
    }

    public void Startup<T>() where T : IStartup
    {
        if (StartupWindowType is not null)
        {
            throw new StartupTypeAlreadyDefinedException(StartupWindowType);
        }

        if (StartupType is not null)
        {
            throw new StartupTypeAlreadyDefinedException(StartupType);
        }

        StartupType = typeof(T);
    }

    private async void StartupAsync(object sender, StartupEventArgs e)
    {
        await _host.StartAsync();

        if (StartupWindowType is not null)
        {
            var dispatcher = Services.GetRequiredService<Dispatcher>();
            var window = (Window)Services.GetRequiredService(StartupWindowType);

            await dispatcher.BeginInvoke(window.Show, DispatcherPriority.ApplicationIdle);
        }
        else if (StartupType is not null)
        {
            var startup = (IStartup)Services.GetRequiredService(StartupType);

            await startup.StartupAsync();
        }
    }

    private async void ExitAsync(object sender, ExitEventArgs e)
    {
        using (_host)
        {
            await _host.StopAsync();
        }
    }
}
