namespace Lexicom.DependencyInjection.Primitives;
public class GuidProvider : IGuidProvider
{
    /// <summary>
    /// A read-only instance of the <see cref="Guid"/> structure whose value is all zeros.
    /// </summary>
    /// <returns>A empty GUID object.</returns>
    public Guid GetEmpty() => Guid.Empty;
    /// <summary>
    /// Initalizes a new instance of the <see cref="Guid"/> structure.
    /// </summary>
    /// <returns>A new GUID object.</returns>
    public Guid NewGuid() => Guid.NewGuid();
}
