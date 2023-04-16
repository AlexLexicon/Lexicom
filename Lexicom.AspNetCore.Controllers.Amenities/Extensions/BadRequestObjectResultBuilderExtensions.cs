using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class BadRequestObjectResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultBuilder AddCode(this IBadRequestObjectResultBuilder builder, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorCode);

        ResultBuilderExtensions.AddCode(builder, errorCode);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultRequestPropertyBuilder FromProperty(
        this IBadRequestObjectResultBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression(nameof(property))] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        return ResultBuilderExtensions.CreateFromProperty<BadRequestObjectResultRequestPropertyBuilder>(propertyString);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultRequestPropertyBuilder FromKey(this IBadRequestObjectResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<BadRequestObjectResultRequestPropertyBuilder>(errorKey);
    }
}
