namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.Extensions;
internal static class AzureOfferingTypeExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static Uri GetServiceEndpoint(this AzureOfferingType azureOfferingType, string workspaceId)
    {
        ArgumentNullException.ThrowIfNull(workspaceId);

        string offeringDomain = azureOfferingType switch
        {
            AzureOfferingType.Public => "azure.com",
            AzureOfferingType.US_Government => "azure.us",
            AzureOfferingType.China => "azure.cn",
            _ => throw new ArgumentOutOfRangeException(nameof(azureOfferingType)),
        };

        return new Uri($"https://{workspaceId}.ods.opinsights.{offeringDomain}/api/logs?api-version=2016-04-01");
    }
}
