using Lexicom.ConsoleApp.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.ConsoleApp.Tui.Extensions;

public static class ConsoleApplicationExtensions
{    
    /// <exception cref="ArgumentNullException"/>
    public static async Task RunLexicomTuiAsync(this ConsoleApplication app, string? title = null)
    {
        ArgumentNullException.ThrowIfNull(app);

        var tuiConsoleApp = app.Services.GetRequiredService<ITuiConsoleApp>();

        await tuiConsoleApp.StartAsync(app.Services, title);
    }
}
