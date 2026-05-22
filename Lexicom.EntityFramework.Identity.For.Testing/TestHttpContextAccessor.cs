using Microsoft.AspNetCore.Http;

namespace Lexicom.EntityFramework.Identity.UnitTesting;

public class TestHttpContextAccessor : IHttpContextAccessor
{
    /// <exception cref="ArgumentNullException"/>
    public TestHttpContextAccessor(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        HttpContext = new DefaultHttpContext
        {
            RequestServices = serviceProvider
        };
    }

    public HttpContext? HttpContext { get; set; }
}
