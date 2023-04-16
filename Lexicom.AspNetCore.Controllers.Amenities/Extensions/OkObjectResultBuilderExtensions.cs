using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class OkObjectResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IOkObjectResultBuilder With<T>(this IOkObjectResultBuilder builder, T value)
    {
        ArgumentNullException.ThrowIfNull(builder);

        if (builder is ObjectResult objectResult)
        {
            objectResult.Value = value;
        }

        return builder;
    }
}
