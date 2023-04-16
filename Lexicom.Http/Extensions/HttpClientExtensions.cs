using Lexicom.Http.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Lexicom.Http.Extensions;
public static class HttpClientExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> DeleteFromJsonNotNullAsync<TValue>(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.DeleteFromJsonAsync<TValue>(requestUri, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> DeleteFromJsonNotNullAsync<TValue>(this HttpClient client, Uri? requestUri, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        TValue? value = await client.DeleteFromJsonAsync(requestUri, jsonTypeInfo, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> DeleteFromJsonNotNullAsync<TValue>(this HttpClient client, Uri? requestUri, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.DeleteFromJsonAsync<TValue>(requestUri, options, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> DeleteFromJsonNotNullAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        TValue? value = await client.DeleteFromJsonAsync(requestUri, jsonTypeInfo, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> DeleteFromJsonNotNullAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.DeleteFromJsonAsync<TValue>(requestUri, options, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> DeleteFromJsonNotNullAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.DeleteFromJsonAsync<TValue>(requestUri, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> GetFromJsonNotNullAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        TValue? value = await client.GetFromJsonAsync(requestUri, jsonTypeInfo, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> GetFromJsonNotNullAsync<TValue>(this HttpClient client, Uri? requestUri, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.GetFromJsonAsync<TValue>(requestUri, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> GetFromJsonNotNullAsync<TValue>(this HttpClient client, Uri? requestUri, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(jsonTypeInfo);

        TValue? value = await client.GetFromJsonAsync(requestUri, jsonTypeInfo, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> GetFromJsonNotNullAsync<TValue>(this HttpClient client, Uri? requestUri, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.GetFromJsonAsync<TValue>(requestUri, options, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> GetFromJsonNotNullAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, JsonSerializerOptions? options, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.GetFromJsonAsync<TValue>(requestUri, options, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="HttpResponseJsonIsNullException"/>
    public static async Task<TValue> GetFromJsonNotNullAsync<TValue>(this HttpClient client, [StringSyntax("Uri")] string? requestUri, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);

        TValue? value = await client.GetFromJsonAsync<TValue>(requestUri, cancellationToken);

        return value ?? throw new HttpResponseJsonIsNullException();
    }
}
