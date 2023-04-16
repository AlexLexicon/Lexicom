using Lexicom.Http.Exceptions;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Lexicom.Http.Extensions;
public static class HttpContentExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> ReadFromJsonNotNullAsync<TValue>(this HttpContent content, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);

        TValue? jsonContent = await HttpContentJsonExtensions.ReadFromJsonAsync<TValue>(content, options, cancellationToken);

        return jsonContent ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> ReadFromJsonNotNullAsync<TValue>(this HttpContent content, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        TValue? jsonContent = await HttpContentJsonExtensions.ReadFromJsonAsync(content, jsonTypeInfo, cancellationToken);

        return jsonContent ?? throw new HttpResponseJsonIsNullException();
    }
}
