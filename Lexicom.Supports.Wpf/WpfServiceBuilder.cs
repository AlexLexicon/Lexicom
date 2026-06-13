using Lexicom.Wpf.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Supports.Wpf;

public interface IWpfServiceBuilder
{
    IServiceCollection Services { get; }
    ConfigurationManager Configuration { get; }
}
public interface IDependentWpfServiceBuilder : IWpfServiceBuilder
{
    WpfApplicationBuilder WpfApplicationBuilder { get; }
}
public class WpfServiceBuilder : IDependentWpfServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public WpfServiceBuilder(WpfApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        WpfApplicationBuilder = builder;
    }

    public WpfApplicationBuilder WpfApplicationBuilder { get; }
    public IServiceCollection Services => WpfApplicationBuilder.Services;
    public ConfigurationManager Configuration => WpfApplicationBuilder.Configuration;
}
