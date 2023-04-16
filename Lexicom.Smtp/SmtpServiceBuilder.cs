using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Smtp;
public interface ISmtpServiceBuilder
{
    IServiceCollection Services { get; }
    IConfiguration Configuration { get; }
}
public class SmtpServiceBuilder : ISmtpServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public SmtpServiceBuilder(
        IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        Services = services;
        Configuration = configuration;
    }

    public IServiceCollection Services { get; }
    public IConfiguration Configuration { get; }
}
