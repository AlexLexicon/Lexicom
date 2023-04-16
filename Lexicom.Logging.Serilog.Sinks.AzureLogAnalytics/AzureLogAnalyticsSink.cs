using Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics.Extensions;
using Newtonsoft.Json;
using Serilog.Core;
using Serilog.Debugging;
using Serilog.Events;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics;
internal sealed class AzureLogAnalyticsSink : AzureLogAnalyticsBatchProvider, ILogEventSink
{
    private static string BuildSignature(int contentLength, string dateString, string key)
    {
        var encoding = new UTF8Encoding();

        string stringToHash = $"POST\n{contentLength}\napplication/json\nx-ms-date:{dateString}\n/api/logs";

        byte[] keyBytes = Convert.FromBase64String(key);
        byte[] messageBytes = encoding.GetBytes(stringToHash);

        using var hmacsha256 = new HMACSHA256(keyBytes);

        byte[] messageHash = hmacsha256.ComputeHash(messageBytes);

        return Convert.ToBase64String(messageHash);
    }

    private const int MESSAGE_SIZE_IN_BYTES_MAXIMUM = 30_000_000;

    private static HttpClient HttpClient { get; } = new HttpClient();

    private readonly string _workSpaceId;
    private readonly string _agentPrimaryOrSecondaryKey;
    private readonly AzureLogAnalyticsSettings _settings;
    private readonly SemaphoreSlim _semaphore;
    private readonly Uri _serviceEndpointUrl;
    private readonly JsonSerializerSettings _generalJsonSerializerSettings;
    private readonly JsonSerializerSettings _logPropertiesjsonSerializerSettings;

    internal AzureLogAnalyticsSink(
        string workSpaceId,
        string agentPrimaryOrSecondaryKey,
        AzureLogAnalyticsSettings settings) : base(settings.BatchSize, settings.BufferSize)
    {
        ArgumentNullException.ThrowIfNull(workSpaceId);
        ArgumentNullException.ThrowIfNull(agentPrimaryOrSecondaryKey);
        ArgumentNullException.ThrowIfNull(settings);

        _workSpaceId = workSpaceId;
        _agentPrimaryOrSecondaryKey = agentPrimaryOrSecondaryKey;
        _settings = settings;
        _semaphore = new SemaphoreSlim(1, 1);
        _serviceEndpointUrl = settings.AzureOfferingType.GetServiceEndpoint(_workSpaceId);
        _generalJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = _settings.LogNamingStrategy.GetContractResolver(),
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            Formatting = Formatting.None,
        };
        _logPropertiesjsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = _settings.LogPropertiesNamingStrategy?.GetContractResolver() ?? _generalJsonSerializerSettings.ContractResolver,
            ReferenceLoopHandling = _generalJsonSerializerSettings.ReferenceLoopHandling,
            PreserveReferencesHandling = _generalJsonSerializerSettings.PreserveReferencesHandling,
            Formatting = _generalJsonSerializerSettings.Formatting,
        };
    }

    /// <exception cref="ArgumentNullException"/>
    public void Emit(LogEvent logEvent)
    {
        ArgumentNullException.ThrowIfNull(logEvent);

        PushEvent(logEvent);
    }

    protected override async Task<bool> WriteLogEventAsync(ICollection<LogEvent>? logEventsBatch)
    {
        if (logEventsBatch is null || !logEventsBatch.Any())
        {
            return true;
        }

        int counter = 0;

        var jsonStringCollection = new List<string>();
        foreach (LogEvent logEvent in logEventsBatch)
        {
            IDictionary<string, object?> logEventPropertiesDictionary = logEvent.ConvertToDictionary(_settings.StoreTimestampInUtc, _settings.LogMessageFormatProvider);
            string serializedLogEventProperties = JsonConvert.SerializeObject(logEventPropertiesDictionary, _generalJsonSerializerSettings);

            IDictionary<string, object?> logMessagePropertiesDictionary = logEvent.Properties.ConvertToDictionary();
            string serializedLogMessageProperties = JsonConvert.SerializeObject(logMessagePropertiesDictionary, _logPropertiesjsonSerializerSettings);

            string logPropertiesName = _settings.LogNamingStrategy switch
            {
                JsonNamingStrategy.LowerCase => "logproperties",
                JsonNamingStrategy.UpperCase => "LOGPROPERTIES",
                JsonNamingStrategy.CamelCase => "logProperties",
                _ => "LogProperties",
            };

            if (!serializedLogEventProperties.EndsWith('}'))
            {
                SelfLog.WriteLine($"The log event json was malformed.");

                continue;
            }

            serializedLogEventProperties = serializedLogEventProperties[..^1];
            serializedLogEventProperties = $"{serializedLogEventProperties},\"{logPropertiesName}\":";
            if (_settings.IsFlattenedProperties)
            {
                string unescapedQuotes = serializedLogMessageProperties.Replace("\\\"", "\"");
                string removedEscapedQuotes = unescapedQuotes.Replace("\"", "\\\"");
                serializedLogEventProperties = $"{serializedLogEventProperties}\"{removedEscapedQuotes}\"";
            }
            else
            {
                serializedLogEventProperties = $"{serializedLogEventProperties}{serializedLogMessageProperties}";
            }
            serializedLogEventProperties = $"{serializedLogEventProperties}}}";

            try
            {
                //check that the json is valid
                System.Text.Json.JsonDocument.Parse(serializedLogEventProperties);
            }
            catch (System.Text.Json.JsonException e)
            {
                SelfLog.WriteLine("The finalized log event json is invalid because {0} at {1}.", e.Message, e.StackTrace);

                continue;
            }

            int serializedEventSizeInBytes = sizeof(char) * serializedLogEventProperties.Length;

            if (serializedEventSizeInBytes >= MESSAGE_SIZE_IN_BYTES_MAXIMUM)
            {
                if (counter > 0)
                {
                    counter--;
                }

                SelfLog.WriteLine("Log size is more than 30 MB. Consider sending smaller message.");
                SelfLog.WriteLine("Dropping invalid log message");

                continue;
            }

            jsonStringCollection.Add(serializedLogEventProperties);
            counter++;
        }

        if (counter < logEventsBatch.Count)
        {
            SelfLog.WriteLine($"Sending mini batch of size {counter}");
        }

        if (!jsonStringCollection.Any())
        {
            return false;
        }

        return await SendJsonAsync(jsonStringCollection).ConfigureAwait(false);
    }

    private async Task<bool> SendJsonAsync(List<string> jsonStringCollection)
    {
        await _semaphore.WaitAsync().ConfigureAwait(false);

        try
        {
            string delimitedJsonStringCollection = string.Join(",", jsonStringCollection.ToArray());

            string jsonArrayStringCollection = $"[{delimitedJsonStringCollection}]";

            int jsonArrayStringCollectionByteCount = Encoding.UTF8.GetByteCount(jsonArrayStringCollection);

            string dateString = DateTime.UtcNow.ToString("r");

            string hashedSignature = BuildSignature(jsonArrayStringCollectionByteCount, dateString, _agentPrimaryOrSecondaryKey);

            string signature = $"SharedKey {_workSpaceId}:{hashedSignature}";

            var stringContent = new StringContent(jsonArrayStringCollection);
            stringContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            stringContent.Headers.Add("Log-Type", _settings.LogName);

            using var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = _serviceEndpointUrl,
                Content = stringContent,
            };
            httpRequestMessage.Headers.Add("Authorization", signature);
            httpRequestMessage.Headers.Add("x-ms-date", dateString);

            using HttpResponseMessage response = await HttpClient.SendAsync(httpRequestMessage);

            if (!response.IsSuccessStatusCode)
            {
                string responseMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                SelfLog.WriteLine("Failed when sending log to '{0}' because '{1}' with the response '{2}'.", _serviceEndpointUrl.ToString(), response.ReasonPhrase, responseMessage);

                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            SelfLog.WriteLine("There was a unexpected error while trying to send the log because '{0}' at {1}.", ex, ex.StackTrace);

            return false;
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
