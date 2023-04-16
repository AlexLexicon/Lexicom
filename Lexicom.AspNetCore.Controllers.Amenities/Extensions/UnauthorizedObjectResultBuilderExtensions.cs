using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class UnauthorizedObjectResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IUnauthorizedObjectResultBuilder AddCode(this IUnauthorizedObjectResultBuilder builder, string errorCode)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorCode);

        ResultBuilderExtensions.AddCode(builder, errorCode);

        return builder;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IUnauthorizedObjectResultRequestPropertyBuilder FromProperty(
        this IUnauthorizedObjectResultBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression(nameof(property))] string argumentString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(argumentString);

        return ResultBuilderExtensions.CreateFromProperty<UnauthorizedObjectResultRequestPropertyBuilder>(argumentString);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IUnauthorizedObjectResultRequestPropertyBuilder FromKey(this IUnauthorizedObjectResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<UnauthorizedObjectResultRequestPropertyBuilder>(errorKey);
    }
}
