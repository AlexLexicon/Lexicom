using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.EntityFramework.UnitTesting;
public interface IEntityFrameworkUnitTestingServiceBuilder
{
    IServiceCollection Services { get; }
    IConfigurationBuilder ConfigurationBuilder { get; }
}
public class EntityFrameworkUnitTestingServiceBuilder : IEntityFrameworkUnitTestingServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public EntityFrameworkUnitTestingServiceBuilder(
        IServiceCollection services,
        IConfigurationBuilder configurationBuilder)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configurationBuilder);

        Services = services;
        ConfigurationBuilder = configurationBuilder;
    }

    public IServiceCollection Services { get; }
    public IConfigurationBuilder ConfigurationBuilder { get; }
}
