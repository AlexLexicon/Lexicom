using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class BadRequestResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultBuilder AddCode(this IBadRequestResultBuilder builder, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorCode);

        return ResultBuilderExtensions.CreateAddCode<BadRequestObjectResultBuilder>(errorCode);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IBadRequestObjectResultRequestPropertyBuilder FromProperty(
        this IBadRequestResultBuilder builder,
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
    public static IBadRequestObjectResultRequestPropertyBuilder FromKey(this IBadRequestResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<BadRequestObjectResultRequestPropertyBuilder>(errorKey);
    }
}
