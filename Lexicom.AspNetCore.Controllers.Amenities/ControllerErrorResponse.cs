using Lexicom.AspNetCore.Controllers.Contracts;

namespace Lexicom.AspNetCore.Controllers.Amenities;
public class ControllerErrorResponse : ErrorResponse
{
    private static ControllerErrorResponse? _unexpectedError;
    public static ControllerErrorResponse UnexpectedError
    {
        get
        {
            if (_unexpectedError is null)
            {
                _unexpectedError = new ControllerErrorResponse();
                _unexpectedError.AddError("Operation Failed", "An unexpected error occured.");
                _unexpectedError.AddCode(CODE_UNEXPECTED);
            }

            return _unexpectedError;
        }
    }

    private readonly Dictionary<string, List<string>> _errors;
    private readonly List<string> _codes;

    public ControllerErrorResponse()
    {
        _errors = new Dictionary<string, List<string>>();
        _codes = new List<string>();
    }

    public new IReadOnlyDictionary<string, IReadOnlyList<string>> Errors => _errors.Where(kvp => kvp.Value.Any()).ToDictionary<KeyValuePair<string, List<string>>, string, IReadOnlyList<string>>(kvp => kvp.Key, kvp => kvp.Value);
    public new IReadOnlyList<string> Codes => _codes;

    /// <exception cref="ArgumentNullException"/>
    public void AddError(string errorKey)
    {
        ArgumentNullException.ThrowIfNull(errorKey);

        if (!_errors.ContainsKey(errorKey))
        {
            _errors.Add(errorKey, new List<string>());
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
            _errors.Add(errorKey, new List<string>
            {
                errorMessage
            });
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
