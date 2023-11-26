using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;

namespace Lexicom.ConsoleApp.DependencyInjection;
public sealed class ConsoleApplicationBuilder
{
    private readonly HostBuilder _hostBuilder;

    internal ConsoleApplicationBuilder()
    {
        Services = new ServiceCollection();
        Configuration = new ConfigurationManager();

        Configuration
            .AddEnvironmentVariables(prefix: "DOTNET_")
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        Environment = CreateHostingEnvironment();

        Configuration.AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

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

    public ConsoleApplication Build()
    {
        IHost host = _hostBuilder.Build();

        return new ConsoleApplication(host, Environment);
    }

    #region Microsoft.Extensions.Hosting.HostBuilder.cs
    //based on microsoft source code: https://source.dot.net/#Microsoft.Extensions.Hosting/HostBuilder.cs
    private HostingEnvironment CreateHostingEnvironment()
    {
        var hostingEnvironment = new HostingEnvironment()
        {
            EnvironmentName = Configuration[HostDefaults.EnvironmentKey] ?? Environments.Production,
            ContentRootPath = ResolveContentRootPath(Configuration[HostDefaults.ContentRootKey], AppContext.BaseDirectory),
        };

        string? applicationName = Configuration[HostDefaults.ApplicationKey];
        if (string.IsNullOrEmpty(applicationName))
        {
            // Note GetEntryAssembly returns null for the net4x console test runner.
            applicationName = Assembly.GetEntryAssembly()?.GetName().Name;
        }

        if (applicationName is not null)
        {
            hostingEnvironment.ApplicationName = applicationName;
        }

        var physicalFileProvider = new PhysicalFileProvider(hostingEnvironment.ContentRootPath);
        hostingEnvironment.ContentRootFileProvider = physicalFileProvider;

        return hostingEnvironment;
    }
    private static string ResolveContentRootPath(string? contentRootPath, string basePath)
    {
        if (string.IsNullOrEmpty(contentRootPath))
        {
            return basePath;
        }
        if (Path.IsPathRooted(contentRootPath))
        {
            return contentRootPath;
        }
        return Path.Combine(Path.GetFullPath(basePath), contentRootPath);
    }
    #endregion
}
