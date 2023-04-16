using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.Supports.Wpf;

namespace Lexicom.DependencyInjection.Primitives.For.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddTimeProvider(this IWpfServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddLexicomTimeProvider();

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddGuidProvider(this IWpfServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddLexicomGuidProvider();

        return builder;
    }
}
