using Lexicom.Wpf.DependencyInjection;

namespace Lexicom.Mvvm.For.Wpf.Extensions;
public static class WpfApplicationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WpfApplication StartupViewModel<TViewModel>(this WpfApplication app) where TViewModel : notnull
    {
        ArgumentNullException.ThrowIfNull(app);

        app.Startup<ViewModelStartup<TViewModel>>();

        return app;
    }
}
