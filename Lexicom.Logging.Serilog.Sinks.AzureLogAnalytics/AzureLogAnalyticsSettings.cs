namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics;
public sealed class AzureLogAnalyticsSettings
{
    private int _logBufferSize = 25_000;
    private int _batchSize = 100;
    private string _logName = "DiagnosticsLog";

    public bool StoreTimestampInUtc { get; set; }
    public IFormatProvider? LogMessageFormatProvider { get; set; }
    public AzureOfferingType AzureOfferingType { get; set; }
    public JsonNamingStrategy LogNamingStrategy { get; set; }
    public JsonNamingStrategy? LogPropertiesNamingStrategy { get; set; }

    public int BufferSize
    {
        get => _logBufferSize;
        set => _logBufferSize = value is >= 5_000 and <= 100_000 ? value : 25_000;
    }

    public int BatchSize
    {
        get => _batchSize;
        set => _batchSize = value is >= 1 and <= 1_000 ? value : 100;
    }

    public string LogName
    {
        get => _logName;
        set => _logName = string.IsNullOrWhiteSpace(value) ? "DiagnosticsLog" : value;
    }

    public bool IsFlattenedProperties { get; set; }
}
