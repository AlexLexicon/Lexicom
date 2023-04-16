using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Lexicom.Extensions.Debugging;
public static class ServiceCollectionExtensions
{
    public static string ToReadableJsonForDebugging(this IServiceCollection services)
    {
        var readableServices = new List<ReadableService>();
        foreach (var service in services)
        {
            readableServices.Add(new ReadableService(GetTypeName(service.ServiceType), GetTypeName(service.ImplementationType), GetTypeName(service.ImplementationInstance?.GetType()), service.Lifetime.ToString()));
        }

        return JsonSerializer.Serialize(readableServices);
    }

    private static string GetTypeName(Type? type)
    {
        if (type is null)
        {
            return "null";
        }

        string typeName = $"{type.Name}";

        Type[] genericArguments = type.GetGenericArguments();
        if (genericArguments.Any())
        {
            int indexOfGenericCountSymbol = typeName.IndexOf('`');
            if (indexOfGenericCountSymbol >= 0)
            {
                //remove the generic count symbol
                typeName = typeName[..indexOfGenericCountSymbol];
            }

            typeName += "<";

            foreach (Type genericArgument in genericArguments)
            {
                typeName += $"{GetTypeName(genericArgument)}, ";
            }

            typeName = $"{typeName.TrimEnd(' ', ',')}>";
        }

        return typeName;
    }

    private record class ReadableService(string ServiceTypeName, string ImplementationTypeName, string ImplementationInstanceTypeName, string Lifetime);
}
