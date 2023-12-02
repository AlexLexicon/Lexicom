using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ForbidObjectResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultBuilder AddCode(this IForbidObjectResultBuilder builder, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorCode);

        ResultBuilderExtensions.AddCode(builder, errorCode);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultRequestPropertyBuilder FromProperty(
        this IForbidObjectResultBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression("property")] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        return ResultBuilderExtensions.CreateFromProperty<ForbidObjectResultRequestPropertyBuilder>(propertyString);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultRequestPropertyBuilder FromKey(this IForbidObjectResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<ForbidObjectResultRequestPropertyBuilder>(errorKey);
    }
}
