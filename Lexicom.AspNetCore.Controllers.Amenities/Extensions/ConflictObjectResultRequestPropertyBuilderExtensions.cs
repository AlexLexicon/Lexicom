using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class ConflictObjectResultRequestPropertyBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultRequestPropertyBuilder FromProperty(
        this IConflictObjectResultRequestPropertyBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression(nameof(property))] string argumentString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(argumentString);

        ResultBuilderExtensions.FromKey(builder, argumentString);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultRequestPropertyBuilder FromKey(this IConflictObjectResultRequestPropertyBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        ResultBuilderExtensions.FromKey(builder, errorKey);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IConflictObjectResultRequestPropertyBuilder WithMessage(this IConflictObjectResultRequestPropertyBuilder builder, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorMessage);

        ResultBuilderExtensions.WithMessage(builder, errorMessage);

        return builder;
    }
}
