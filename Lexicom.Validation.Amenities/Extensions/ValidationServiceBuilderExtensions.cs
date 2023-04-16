namespace Lexicom.Validation.Amenities.Extensions;
public static class ValidationServiceBuilderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IValidationServiceBuilder AddAmenities(this IValidationServiceBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddLexicomValidationAmenities();

        return builder;
    }
}