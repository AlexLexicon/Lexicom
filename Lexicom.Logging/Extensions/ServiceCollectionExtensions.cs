using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Lexicom.Logging.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomLogging(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        Log.CloseAndFlush();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        services.AddLogging(configure =>
        {
            configure.AddSerilog();
        });

        return services;
    }
}
