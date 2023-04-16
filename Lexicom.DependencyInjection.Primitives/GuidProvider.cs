namespace Lexicom.DependencyInjection.Primitives;
public class GuidProvider : IGuidProvider
{
    public Guid Empty => Guid.Empty;
    public Guid NewGuid() => Guid.NewGuid();
}
