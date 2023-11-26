using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace Lexicom.EntityFramework.Identity;
public class IdentityResultErrorsCollection : IEnumerable<IdentityError>
{
    private readonly IReadOnlyList<IdentityError> _identityErrors;

    /// <exception cref="ArgumentNullException"/>
    public IdentityResultErrorsCollection(IEnumerable<IdentityError> identityErrors)
    {
        ArgumentNullException.ThrowIfNull(identityErrors);

        _identityErrors = identityErrors.Where(ie => ie is not null).ToList();
    }

    public override string ToString()
    {
        List<string> errorStrings = _identityErrors.Select(ie => $"[{ie.Code}: {ie.Description}]").ToList();

        if (errorStrings.Count is not 0)
        {
            if (errorStrings.Count is 1)
            {
                return errorStrings.First();
            }
            else
            {
                return string.Join(",", errorStrings);
            }
        }

        return $"There were no identity errors.";
    }

    public IEnumerator<IdentityError> GetEnumerator() => _identityErrors.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
