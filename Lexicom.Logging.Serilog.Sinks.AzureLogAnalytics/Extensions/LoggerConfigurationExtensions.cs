using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog;

namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.Extensions;
public static class LoggerConfigurationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static LoggerConfiguration AzureLogAnalytics(
        this LoggerSinkConfiguration loggerConfiguration,
        string workspaceId,
        string agentPrimaryOrSecondaryKey,
        string logName = "DiagnosticsLog",
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        bool storeTimestampInUtc = true,
        IFormatProvider? formatProvider = null,
        int logBufferSize = 2000,
        int batchSize = 100,
        AzureOfferingType azureOfferingType = AzureOfferingType.Public,
        LoggingLevelSwitch? levelSwitch = null,
        JsonNamingStrategy logNamingStrategy = JsonNamingStrategy.Default,
        JsonNamingStrategy? logPropertiesNamingStrategy = null,
        bool isFlattenedProperties = true)
    {
        ArgumentNullException.ThrowIfNull(loggerConfiguration);
        ArgumentNullException.ThrowIfNull(workspaceId);
        ArgumentNullException.ThrowIfNull(agentPrimaryOrSecondaryKey);
        ArgumentNullException.ThrowIfNull(logName);

        var configurationSettings = new AzureLogAnalyticsSettings
        {
            LogMessageFormatProvider = formatProvider,
            StoreTimestampInUtc = storeTimestampInUtc,
            AzureOfferingType = azureOfferingType,
            BufferSize = logBufferSize,
            BatchSize = batchSize,
            LogName = logName,
            LogNamingStrategy = logNamingStrategy,
            LogPropertiesNamingStrategy = logPropertiesNamingStrategy,
            IsFlattenedProperties = isFlattenedProperties,
        };

        return AzureLogAnalytics(loggerConfiguration, workspaceId, agentPrimaryOrSecondaryKey, configurationSettings, restrictedToMinimumLevel, levelSwitch);
    }

    /// <exception cref="ArgumentNullException"/>
    public static LoggerConfiguration AzureLogAnalytics(
        this LoggerSinkConfiguration loggerConfiguration, 
        string workspaceId, 
        string agentPrimaryOrSecondaryKey, 
        AzureLogAnalyticsSettings settings, 
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum, 
        LoggingLevelSwitch? levelSwitch = null)
    {
        ArgumentNullException.ThrowIfNull(loggerConfiguration);
        ArgumentNullException.ThrowIfNull(workspaceId);
        ArgumentNullException.ThrowIfNull(agentPrimaryOrSecondaryKey);
        ArgumentNullException.ThrowIfNull(settings);

        var azureLogAnalyticsSink = new AzureLogAnalyticsSink(workspaceId, agentPrimaryOrSecondaryKey, settings);

        return loggerConfiguration.Sink(azureLogAnalyticsSink, restrictedToMinimumLevel, levelSwitch);
    }
}
