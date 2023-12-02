using Lexicom.AspNetCore.Controllers.Amenities.ActionResultBuilders;
using System.Runtime.CompilerServices;

namespace Lexicom.AspNetCore.Controllers.Amenities.Extensions;
public static class UnauthorizedResultBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IUnauthorizedObjectResultBuilder AddCode(this IUnauthorizedResultBuilder builder, string code)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(code);

        return ResultBuilderExtensions.CreateAddCode<UnauthorizedObjectResultBuilder>(code);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IUnauthorizedObjectResultRequestPropertyBuilder FromProperty(
        this IUnauthorizedResultBuilder builder,
#pragma warning disable IDE0060 // Remove unused parameter
        object? property,
#pragma warning restore IDE0060 // Remove unused parameter
        [CallerArgumentExpression("property")] string propertyString = "")
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(propertyString);

        return ResultBuilderExtensions.CreateFromProperty<UnauthorizedObjectResultRequestPropertyBuilder>(propertyString);
    }

    /// <exception cref="ArgumentNullException"/>
    public static IUnauthorizedObjectResultRequestPropertyBuilder FromKey(this IUnauthorizedResultBuilder builder, string errorKey)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentNullException.ThrowIfNull(errorKey);

        return ResultBuilderExtensions.CreateFromKey<UnauthorizedObjectResultRequestPropertyBuilder>(errorKey);
    }
}
