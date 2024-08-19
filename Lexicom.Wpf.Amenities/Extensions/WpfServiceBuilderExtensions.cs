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

        builder.Services.AddSingleton<IWindowsDialog, WpfWindowsDialog>();
        builder.Services.AddSingleton<IThemeApplicator, WpfThemeApplicator>();
        builder.Services.AddSingleton<IThemeProvider, WpfThemeProvider>();
        builder.Services.AddSingleton<IDispatcher>(sp =>
        {
            var dispatcher = sp.GetRequiredService<Dispatcher>();

            return new WpfDispatcher(dispatcher);
        });

        return builder;
    }
}