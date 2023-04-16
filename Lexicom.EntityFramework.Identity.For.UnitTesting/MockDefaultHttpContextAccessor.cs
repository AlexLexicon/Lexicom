using Microsoft.AspNetCore.Http;

namespace Lexicom.EntityFramework.Identity.UnitTesting;
public class MockDefaultHttpContextAccessor : IHttpContextAccessor
{
    /// <exception cref="ArgumentNullException"/>
    public MockDefaultHttpContextAccessor(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        HttpContext = new DefaultHttpContext
        {
            RequestServices = serviceProvider
        };
    }

    public HttpContext? HttpContext { get; set; }
}
