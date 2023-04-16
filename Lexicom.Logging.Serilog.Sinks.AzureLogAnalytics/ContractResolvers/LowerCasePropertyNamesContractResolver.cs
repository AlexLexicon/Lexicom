using Newtonsoft.Json.Serialization;

namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.ContractResolvers;
internal sealed class LowerCasePropertyNamesContractResolver : DefaultContractResolver
{
    /// <exception cref="ArgumentNullException"/>
    protected override string ResolvePropertyName(string propertyName)
    {
        ArgumentNullException.ThrowIfNull(propertyName);

        string resolvedPropertyName = base.ResolvePropertyName(propertyName);

        return resolvedPropertyName.ToLowerInvariant();
    }
}