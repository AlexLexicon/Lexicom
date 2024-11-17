using FluentValidation;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class FileExistsValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        return File.Exists(value);
    }
}
public class FileExistsPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(FileExistsPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be an existing file path.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return FileExistsValidator.IsValid(value);
    }
}