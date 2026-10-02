namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics;

public sealed class AzureLogAnalyticsSettings
{
    public bool StoreTimestampInUtc { get; set; }
    public IFormatProvider? LogMessageFormatProvider { get; set; }
    public AzureOfferingType AzureOfferingType { get; set; }
    public JsonNamingStrategy LogNamingStrategy { get; set; }
    public JsonNamingStrategy? LogPropertiesNamingStrategy { get; set; }

    private int _bufferSize = Constants.BUFFER_SIZE_DEFAULT;
    public int BufferSize
    {
        get => _bufferSize;
        set => _bufferSize = value is >= Constants.BUFFER_SIZE_MINIMUM and <= Constants.BUFFER_SIZE_MAXIMUM ? value : Constants.BUFFER_SIZE_DEFAULT;
    }

    private int _batchSize = Constants.BATCH_SIZE_DEFAULT;
    public int BatchSize
    {
        get => _batchSize;
        set => _batchSize = value is >= Constants.BATCH_SIZE_MINIMUM and <= Constants.BATCH_SIZE_MAXIMUM ? value : Constants.BATCH_SIZE_DEFAULT;
    }

    public bool IsFlattenedProperties { get; set; }
}
