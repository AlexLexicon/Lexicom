using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ConflictResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultBuilder AddCode(this IConflictResultBuilder builder, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorCode);

        return ResultBuilderExtensions.CreateAddCode<ConflictObjectResultBuilder>(errorCode);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultRequestPropertyBuilder FromProperty(
        this IConflictResultBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression(nameof(property))] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        return ResultBuilderExtensions.CreateFromProperty<ConflictObjectResultRequestPropertyBuilder>(propertyString);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultRequestPropertyBuilder FromKey(this IConflictResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<ConflictObjectResultRequestPropertyBuilder>(errorKey);
    }
}
