using Serilog.Debugging;
using Serilog.Events;
using System.Dynamic;

namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.Extensions;
internal static class LogEventExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IDictionary<string, object?> ConvertToDictionary(this LogEvent logEvent, bool storeTimestampInUtc, IFormatProvider? logMessageFormatProvider)
    {
        ArgumentNullException.ThrowIfNull(logEvent);

        IDictionary<string, object?> eventObject = new ExpandoObject();

        string timestampString = storeTimestampInUtc ? logEvent.Timestamp.ToUniversalTime().ToString("o") : logEvent.Timestamp.ToString("o");

        eventObject.Add("Timestamp", timestampString);
        eventObject.Add("LogLevel", logEvent.Level.ToString());
        eventObject.Add("LogMessageTemplate", logEvent.MessageTemplate.Text);
        eventObject.Add("LogMessage", logEvent.RenderMessage(logMessageFormatProvider));
        eventObject.Add("LogException", logEvent.Exception);

        return eventObject;
    }
    /// <exception cref="ArgumentNullException"/>
    public static IDictionary<string, object?> ConvertToDictionary(this IReadOnlyDictionary<string, LogEventPropertyValue> properties)
    {
        ArgumentNullException.ThrowIfNull(properties);

        IDictionary<string, object?> expObject = new ExpandoObject();

        foreach (var property in properties)
        {
            expObject.Add(property.Key, Simplify(property.Value));
        }

        return expObject;
    }

    private static object? Simplify(LogEventPropertyValue data)
    {
        if (data is ScalarValue value)
        {
            return value.Value;
        }

        // ReSharper disable once SuspiciousTypeConversion.Global
        if (data is DictionaryValue dictValue)
        {
            IDictionary<string, object?> expObject = new ExpandoObject();

            foreach (KeyValuePair<ScalarValue, LogEventPropertyValue> element in dictValue.Elements)
            {
                if (element.Key.Value is string key)
                {
                    expObject.Add(key, Simplify(element.Value));
                }
            }

            return expObject;
        }

        if (data is SequenceValue seq)
        {
            return seq.Elements.Select(Simplify).ToArray();
        }

        if (data is not StructureValue str)
        {
            return null;
        }

        try
        {
            if (str.TypeTag is null)
            {
                return str.Properties.ToDictionary(p => p.Name, p => Simplify(p.Value));
            }

            if (!str.TypeTag.StartsWith("DictionaryEntry") && !str.TypeTag.StartsWith("KeyValuePair"))
            {
                return str.Properties.ToDictionary(p => p.Name, p => Simplify(p.Value));
            }

            object? key = Simplify(str.Properties[0].Value);
            string? keyString = key?.ToString();

            if (keyString is null)
            {
                return null;
            }

            IDictionary<string, object?> expObject = new ExpandoObject();

            expObject.Add(keyString, Simplify(str.Properties[1].Value));

            return expObject;
        }
        catch (Exception ex)
        {
            SelfLog.WriteLine("There was a unexpected error while trying to simplify LogEventPropertyValue data because '{0}' at {1}.", ex, ex.StackTrace);
        }

        return null;
    }
}
