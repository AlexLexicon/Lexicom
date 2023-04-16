using FluentValidation.Results;

namespace Lexicom.Validation.Extensions;
public static class ValidationFailureExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IReadOnlyList<string> ToErrorMessages(this IEnumerable<ValidationFailure> validationFailures)
    {
        ArgumentNullException.ThrowIfNull(validationFailures);

        return validationFailures.Select(vf => vf.ErrorMessage).ToList();
    }

    /// <exception cref="ArgumentNullException"/>
    public static ValidationFailure StandardizeErrorMessage(this ValidationFailure validationFailure)
    {
        ArgumentNullException.ThrowIfNull(validationFailure);

        string message = validationFailure.ErrorMessage;

        //remove the word must from the message
        int mustIndex = message.IndexOf("must", StringComparison.OrdinalIgnoreCase);
        if (mustIndex > 0)
        {
            message = message[mustIndex..];
        }

        //remove empty quotes
        const string MESSAGE_EMPTY_NAME_START = "'' ";
        if (message.Contains(MESSAGE_EMPTY_NAME_START))
        {
            message = message.Replace(MESSAGE_EMPTY_NAME_START, string.Empty);
        }

        const string MESSAGE_EMPTY_NAME_END = " ''";
        if (message.Contains(MESSAGE_EMPTY_NAME_END))
        {
            message = message.Replace(MESSAGE_EMPTY_NAME_END, string.Empty);
        }

        //remove extra spaces
        message = RemoveSpacesBetweenWords(message);

        if (!string.IsNullOrWhiteSpace(message))
        {
            //captialize the first letter of the message
            message = char.ToUpper(message[0]) + message[1..];
        }
        else
        {
            message = string.Empty;
        }

        validationFailure.ErrorMessage = message;

        return validationFailure;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IReadOnlyList<ValidationFailure> StandardizeErrorMessages(this IEnumerable<ValidationFailure> validationFailures)
    {
        ArgumentNullException.ThrowIfNull(validationFailures);

        ValidationFailure[] validationFailuresArray = validationFailures.ToArray();

        var sanitizedFailures = new List<ValidationFailure>();
        for (int index = 0; index < validationFailuresArray.Length; index++)
        {
            ValidationFailure validationFailure = validationFailuresArray[index];

            validationFailure = StandardizeErrorMessage(validationFailure);

            string message =  validationFailure.ErrorMessage;

            if (!string.IsNullOrWhiteSpace(message))
            {
                //we check for duplicates after parsing the error messages
                //because some messages will become the same only after we parse them
                bool alreadyExists = sanitizedFailures.Any(vf => vf.PropertyName == validationFailure.PropertyName && vf.ErrorMessage == validationFailure.ErrorMessage);
                if (!alreadyExists)
                {
                    sanitizedFailures.Add(validationFailure);
                }
            }
        }

        return sanitizedFailures;
    }

    /// <exception cref="ArgumentNullException"/>
    public static ValidationFailure SanitizeErrorMessage(this ValidationFailure validationFailure)
    {
        ArgumentNullException.ThrowIfNull(validationFailure);

        string errorMessage = validationFailure.ErrorMessage;

        string clearErrorMessage = string.Empty;
        bool isSkip = false;
        foreach (char character in errorMessage)
        {
            if (character == '\'')
            {
                isSkip = !isSkip;
            }
            else if (!isSkip)
            {
                clearErrorMessage += character;
            }
        }

        clearErrorMessage = RemoveSpacesBetweenWords(clearErrorMessage);

        validationFailure.ErrorMessage = clearErrorMessage;

        return validationFailure;
    }

    /// <exception cref="ArgumentNullException"/>
    public static IReadOnlyList<ValidationFailure> SanitizeErrorMessages(this IEnumerable<ValidationFailure> validationFailures)
    {
        ArgumentNullException.ThrowIfNull(validationFailures);

        ValidationFailure[] validationFailuresArray = validationFailures.ToArray();

        var frontEndErrorMessages = new List<ValidationFailure>();
        for (int index = 0; index < validationFailuresArray.Length; index++)
        {
            ValidationFailure validationFailure = validationFailuresArray[index];

            validationFailure = validationFailure.SanitizeErrorMessage();

            frontEndErrorMessages.Add(validationFailure);
        }

        return frontEndErrorMessages;
    }

    private static string RemoveSpacesBetweenWords(string text)
    {
        return string
            .Join(" ", text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
            .Trim();
    }
}