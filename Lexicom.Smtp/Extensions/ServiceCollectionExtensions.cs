using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Smtp.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLexicomSmtp(this IServiceCollection services, IConfiguration configuration, Action<ISmtpServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        configure?.Invoke(new SmtpServiceBuilder(services, configuration));

        return services;
    }
}
