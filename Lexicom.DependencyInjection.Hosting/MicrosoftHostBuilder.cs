using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System.Reflection;

namespace Lexicom.DependencyInjection.Hosting;
public static class MicrosoftHostBuilder
{
    //Microsoft.Extensions.Hosting.HostBuilder.cs
    //based on microsoft source code: https://source.dot.net/#Microsoft.Extensions.Hosting/HostBuilder.cs
    public static HostingEnvironment CreateHostingEnvironment(ConfigurationManager configuration)
    {
        var hostingEnvironment = new HostingEnvironment()
        {
            EnvironmentName = configuration[HostDefaults.EnvironmentKey] ?? Environments.Production,
            ContentRootPath = ResolveContentRootPath(configuration[HostDefaults.ContentRootKey], AppContext.BaseDirectory),
        };

        string? applicationName = configuration[HostDefaults.ApplicationKey];
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
}
