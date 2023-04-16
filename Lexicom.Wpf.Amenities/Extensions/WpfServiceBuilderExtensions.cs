using Lexicom.Supports.Wpf;
using Lexicom.Wpf.Amenities.Dialogs;
using Lexicom.Wpf.Amenities.Themes;
using Lexicom.Wpf.Amenities.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Threading;

namespace Lexicom.Wpf.Amenities.Extensions;
public static class WpfServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IWpfServiceBuilder AddAmenities(this IWpfServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.WpfApplicationBuilder.Services.AddSingleton<IWindowsDialog, WpfWindowsDialog>();
        builder.WpfApplicationBuilder.Services.AddSingleton<IThemeApplicator, WpfThemeApplicator>();
        builder.WpfApplicationBuilder.Services.AddSingleton<IThemeProvider, WpfThemeProvider>();
        builder.WpfApplicationBuilder.Services.AddSingleton<IDispatcher>(sp =>
        {
            var dispatcher = sp.GetRequiredService<Dispatcher>();

            return new WpfDispatcher(dispatcher);
        });

        return builder;
    }
}