using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Lexicom.Swashbuckle.Extensions;
public static class WebApplicationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void UseLexicomSwaggerUI(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
