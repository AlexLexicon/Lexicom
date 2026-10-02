using Lexicom.AspNetCore.Controllers.Contracts;

namespace Lexicom.AspNetCore.Controllers.Amenities;

public class ControllerErrorResponse : ErrorResponse
{
    public static ControllerErrorResponse UnexpectedError
    {
        get
        {
            if (field is null)
            {
                field = new ControllerErrorResponse();
                field.AddError("Operation Failed", "An unexpected error occurred.");
                field.AddCode(CODE_UNEXPECTED);
            }

            return field;
        }
    }

    private readonly Dictionary<string, List<string>> _errors;
    private readonly List<string> _codes;

    public ControllerErrorResponse()
    {
        _errors = [];
        _codes = [];
    }

    public new IReadOnlyDictionary<string, IReadOnlyList<string>> Errors
    {
        get
        {
            return _errors
                .Where(kvp => kvp.Value.Count is not 0)
                .ToDictionary<KeyValuePair<string, List<string>>, string, IReadOnlyList<string>>(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
    public new IReadOnlyList<string> Codes => _codes;

    /// <exception cref="ArgumentNullException"/>
    public void AddError(string errorKey)
    {
        ArgumentNullException.ThrowIfNull(errorKey);

        if (!_errors.ContainsKey(errorKey))
        {
            _errors.Add(errorKey, []);
        }
    }
    /// <exception cref="ArgumentNullException"/>
    public void AddError(string errorKey, string errorMessage)
    {
        ArgumentNullException.ThrowIfNull(errorKey);
        ArgumentNullException.ThrowIfNull(errorMessage);

        if (_errors.TryGetValue(errorKey, out List<string>? errorMessages))
        {
            errorMessages.Add(errorMessage);
        }
        else
        {
            _errors.Add(errorKey,
            [
                errorMessage
            ]);
        }
    }

    /// <exception cref="ArgumentNullException"/>
    public void AddCode(string errorCode)
    {
        ArgumentNullException.ThrowIfNull(errorCode);

        if (!_codes.Contains(errorCode))
        {
            _codes.Add(errorCode);
        }
    }
}
