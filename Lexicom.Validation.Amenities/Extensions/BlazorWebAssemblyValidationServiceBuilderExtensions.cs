namespace Lexicom.Validation.Amenities.Extensions;
public static class BlazorWebAssemblyValidationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IBlazorWebAssemblyValidationServiceBuilder AddAmenities(this IBlazorWebAssemblyValidationServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        ValidationServiceBuilderExtensions.AddAmenities(builder);

        return builder;
    }
}
