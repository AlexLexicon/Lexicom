using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class BadRequestObjectResultRequestPropertyBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultRequestPropertyBuilder FromProperty(
        this IBadRequestObjectResultRequestPropertyBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression("property")] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        ResultBuilderExtensions.FromKey(builder, propertyString);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultRequestPropertyBuilder FromKey(this IBadRequestObjectResultRequestPropertyBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        ResultBuilderExtensions.FromKey(builder, errorKey);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultRequestPropertyBuilder WithMessage(this IBadRequestObjectResultRequestPropertyBuilder builder, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorMessage);

        ResultBuilderExtensions.WithMessage(builder, errorMessage);

        return builder;
    }
}
