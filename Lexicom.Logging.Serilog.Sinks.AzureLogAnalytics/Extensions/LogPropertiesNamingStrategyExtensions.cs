using Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.ContractResolvers;
using Newtonsoft.Json.Serialization;

namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.Extensions;
public static class LogPropertiesNamingStrategyExtensions
{
    public static IContractResolver GetContractResolver(this JsonNamingStrategy namingStrategy)
    {
        return namingStrategy switch
        {
            JsonNamingStrategy.LowerCase => new LowerCasePropertyNamesContractResolver(),
            JsonNamingStrategy.UpperCase => new UpperCasePropertyNamesContractResolver(),
            JsonNamingStrategy.CamelCase => new CamelCasePropertyNamesContractResolver(),
            JsonNamingStrategy.PascelCase => new PascelCasePropertyNamesContractResolver(),
            _ => new DefaultContractResolver(),
        };
    }
}
