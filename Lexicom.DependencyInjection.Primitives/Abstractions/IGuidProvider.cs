namespace Lexicom.DependencyInjection.Primitives;
public interface IGuidProvider
{
    Guid Empty { get; }
    Guid NewGuid();
}
