namespace Lexicom.DependencyInjection.Primitives;
public interface IGuidProvider
{
    /// <summary>
    /// A read-only instance of the <see cref="Guid"/> structure whose value is all zeros.
    /// </summary>
    /// <returns>A empty GUID object.</returns>
    Guid GetEmpty();
    /// <summary>
    /// Initalizes a new instance of the <see cref="Guid"/> structure.
    /// </summary>
    /// <returns>A new GUID object.</returns>
    Guid NewGuid();
}
