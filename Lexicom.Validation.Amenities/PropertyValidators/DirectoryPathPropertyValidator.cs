using FluentValidation;
using System.Security;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class DirectoryPathValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        DirectoryInfo? directoryInfo = null;
        try
        {
            directoryInfo = new DirectoryInfo(value);
        }
        catch (ArgumentNullException)
        {
        }
        catch (SecurityException)
        {
        }
        catch (ArgumentException)
        {
        }
        catch (PathTooLongException)
        {
        }

        return directoryInfo is not null;
    }
}
public class DirectoryPathPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(DirectoryPathPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be a valid directory path.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return DirectoryPathValidator.IsValid(value);
    }
}