using Lexicom.Logging.Extensions;
using Lexicom.Supports.Wpf;
using Microsoft.Extensions.Configuration;

namespace Lexicom.Logging.For.Wpf.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddLogging(this IWpfServiceBuilder builder, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomLogging(configuration);

        return builder;
    }
}
