using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ForbidResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultBuilder AddCode(this IForbidResultBuilder builder, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorCode);

        return ResultBuilderExtensions.CreateAddCode<ForbidObjectResultBuilder>(errorCode);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultRequestPropertyBuilder FromProperty(
        this IForbidResultBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression(nameof(property))] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        return ResultBuilderExtensions.CreateFromProperty<ForbidObjectResultRequestPropertyBuilder>(propertyString);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultRequestPropertyBuilder FromKey(this IForbidResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<ForbidObjectResultRequestPropertyBuilder>(errorKey);
    }
}
