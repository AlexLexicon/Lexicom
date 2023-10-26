namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics;
internal static class Constants
{
    public const int BUFFER_SIZE_MINIMUM = 5_000;
    public const int BUFFER_SIZE_MAXIMUM = 100_000;
    public const int BUFFER_SIZE_DEFAULT = 25_000;

    public const int BATCH_SIZE_MINIMUM = 1;
    public const int BATCH_SIZE_MAXIMUM = 1000;
    public const int BATCH_SIZE_DEFAULT = 100;
}
