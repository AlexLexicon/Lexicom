namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics;

public sealed class AzureLogAnalyticsSettings
{
    public bool StoreTimestampInUtc { get; set; }
    public IFormatProvider? LogMessageFormatProvider { get; set; }
    public AzureOfferingType AzureOfferingType { get; set; }
    public JsonNamingStrategy LogNamingStrategy { get; set; }
    public JsonNamingStrategy? LogPropertiesNamingStrategy { get; set; }

    public int BufferSize
    {
        get;
        set => field = value is >= Constants.BUFFER_SIZE_MINIMUM and <= Constants.BUFFER_SIZE_MAXIMUM ? value : Constants.BUFFER_SIZE_DEFAULT;
    } = Constants.BUFFER_SIZE_DEFAULT;

    public int BatchSize
    {
        get;
        set => field = value is >= Constants.BATCH_SIZE_MINIMUM and <= Constants.BATCH_SIZE_MAXIMUM ? value : Constants.BATCH_SIZE_DEFAULT;
    } = Constants.BATCH_SIZE_DEFAULT;

    public bool IsFlattenedProperties { get; set; }
}
