using Lexicom.AspNetCore.Controllers.Amenities.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class WebApplicationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void UseLexicomExceptionHandlingMiddleware(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
