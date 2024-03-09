using System.Collections;

namespace Lexicom.DependencyInjection.Amenities;
public class AssemblyScanResult : IEnumerable<Type>
{
    public static implicit operator Type[](AssemblyScanResult result) => result.ToArray();

    private readonly List<Type> _types;

    /// <exception cref="ArgumentNullException"/>
    public AssemblyScanResult(IEnumerable<Type> types)
    {
        ArgumentNullException.ThrowIfNull(types);

        _types = types.ToList();
    }

    public IEnumerator<Type> GetEnumerator() => _types.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
