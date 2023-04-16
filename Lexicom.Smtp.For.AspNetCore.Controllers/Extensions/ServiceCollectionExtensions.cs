using Lexicom.Smtp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Threading.Channels;

namespace Lexicom.Smtp.AspNetCore.Controllers.Extensions;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IServiceCollection AddLexicomAspNetCoreControllersSmtp(this IServiceCollection services, IConfiguration configuration, Action<ISmtpServiceBuilder>? configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddLexicomSmtp(configuration, configure);

        services.AddSingleton<ChannelSmtpEmailHandler>();
        services.Replace(new ServiceDescriptor(typeof(ISmtpEmailHandler), sp =>
        {
            return sp.GetRequiredService<ChannelSmtpEmailHandler>();
        }, ServiceLifetime.Singleton));

        services.AddSingleton(sp =>
        {
            return Channel.CreateUnbounded<SmtpEmailChannelMessage>();
        });

        services.AddHostedService<ChannelSmtpEmailHostedService>();

        return services;
    }
}
