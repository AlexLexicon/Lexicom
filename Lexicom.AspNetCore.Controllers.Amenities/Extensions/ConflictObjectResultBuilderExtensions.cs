using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ConflictObjectResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultBuilder AddCode(this IConflictObjectResultBuilder builder, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorCode);

        ResultBuilderExtensions.AddCode(builder, errorCode);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultRequestPropertyBuilder FromProperty(
        this IConflictObjectResultBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression("property")] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        return ResultBuilderExtensions.CreateFromProperty<ConflictObjectResultRequestPropertyBuilder>(propertyString);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultRequestPropertyBuilder FromKey(this IConflictObjectResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<ConflictObjectResultRequestPropertyBuilder>(errorKey);
    }
}
