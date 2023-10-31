using Lexicom.Extensions.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Lexicom.Extensions.Debugging;
public static class ServiceCollectionExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static string ToReadableJsonForDebugging(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        var readableServices = new List<ReadableService>();
        foreach (ServiceDescriptor service in services)
        {
            Type? ImplementationInstanceType = service.ImplementationInstance?.GetType();

            string serviceTypeName = service.ServiceType.GetFriendlyName();
            string implementationTypeName = service.ImplementationType.GetFriendlyName();
            string implementationInstanceTypeName = ImplementationInstanceType.GetFriendlyName();
            string lifetimeName = service.Lifetime.ToString();

            readableServices.Add(new ReadableService(serviceTypeName, implementationTypeName, implementationInstanceTypeName, lifetimeName));
        }

        return JsonSerializer.Serialize(readableServices);
    }
}
