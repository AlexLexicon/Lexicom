using System.Net.Http.Json;

namespace Lexicom.AspNetCore.Controllers.Contracts.Extensions;
public static class HttpResponseMessageExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static async Task<ErrorResponse?> TryToErrorResponseAsync(this HttpResponseMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (!message.IsSuccessStatusCode)
        {
            try
            {
                return await message.Content.ReadFromJsonAsync<ErrorResponse>();
            }
            catch
            {

            }
        }

        return null;
    }
}
