using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Lexicom.Logging.AspNetCore.Controllers.Extensions;
public static class WebApplicationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static WebApplication UseLexicomLogging(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseSerilogRequestLogging();

        return app;
    }
}
