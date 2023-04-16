using Lexicom.Wpf.DependencyInjection;

namespace Lexicom.Supports.Wpf;
public interface IWpfServiceBuilder
{
    WpfApplicationBuilder WpfApplicationBuilder { get; }
}
public class WpfServiceBuilder : IWpfServiceBuilder
{
    /// <exception cref="ArgumentNullException"/>
    public WpfServiceBuilder(WpfApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        WpfApplicationBuilder = builder;
    }

    public WpfApplicationBuilder WpfApplicationBuilder { get; }
}
