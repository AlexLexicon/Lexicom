using FluentValidation;
using System.Security;

namespace Lexicom.Validation.Amenities.PropertyValidators;
public class FilePathValidator
{
    public static bool IsValid(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return true;
        }

        FileInfo? fileInfo = null;
        try
        {
            fileInfo = new FileInfo(value);
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
        catch (UnauthorizedAccessException)
        { 
        }
        catch (PathTooLongException)
        {
        }
        catch (NotSupportedException)
        {
        }

        return fileInfo is not null;
    }
}
public class FilePathPropertyValidator<T> : AbstractPropertyValidator<T, string?>
{
    public const string NAME = nameof(FilePathPropertyValidator<T>);
    public const string DEFAULT_MESSAGE_TEMPLATE = "'{PropertyName}' must be a valid file path.";

    public override string Name { get; } = NAME;
    public override string DefaultMessageTemplate { get; } = DEFAULT_MESSAGE_TEMPLATE;

    /// <exception cref="ArgumentNullException"/>
    public override bool IsValid(ValidationContext<T> context, string? value)
    {
        ArgumentNullException.ThrowIfNull(context);

        return FilePathValidator.IsValid(value);
    }
}