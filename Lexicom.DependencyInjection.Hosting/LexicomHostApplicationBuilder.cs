using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting.Internal;

namespace Lexicom.DependencyInjection.Hosting;
public static class LexicomHostApplicationBuilder
{
    public static HostingEnvironment InitalizeDefaultConfigurationProvidersAndEnviornment(ConfigurationManager configuration)
    {
        configuration
            .AddEnvironmentVariables(prefix: "DOTNET_")
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        var environment = MicrosoftHostBuilder.CreateHostingEnvironment(configuration);

        configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        return environment;
    }
}
