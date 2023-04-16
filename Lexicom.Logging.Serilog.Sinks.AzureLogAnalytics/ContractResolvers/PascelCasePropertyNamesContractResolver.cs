using Newtonsoft.Json.Serialization;

namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.ContractResolvers;
internal sealed class PascelCasePropertyNamesContractResolver : DefaultContractResolver
{
    /// <exception cref="ArgumentNullException"/>
    protected override string ResolvePropertyName(string propertyName)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        string resolvedPropertyName = base.ResolvePropertyName(propertyName);

        if (string.IsNullOrEmpty(resolvedPropertyName))
        {
            return resolvedPropertyName;
        }

        return char.ToUpperInvariant(resolvedPropertyName[0]) + resolvedPropertyName[1..];
    }
}