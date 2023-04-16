using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ForbidObjectResultRequestPropertyBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultRequestPropertyBuilder FromProperty(
        this IForbidObjectResultRequestPropertyBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression(nameof(property))] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        ResultBuilderExtensions.FromKey(builder, propertyString);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultRequestPropertyBuilder FromKey(this IForbidObjectResultRequestPropertyBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        ResultBuilderExtensions.FromKey(builder, errorKey);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IForbidObjectResultRequestPropertyBuilder WithMessage(this IForbidObjectResultRequestPropertyBuilder builder, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorMessage);

        ResultBuilderExtensions.WithMessage(builder, errorMessage);

        return builder;
    }
}
