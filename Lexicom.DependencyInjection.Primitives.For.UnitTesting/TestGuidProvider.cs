namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting;
public class TestGuidProvider : IGuidProvider
{
    protected readonly Queue<Guid> _guids;

    public TestGuidProvider()
    {
        _guids = new Queue<Guid>();
    }

    public virtual Guid GetEmpty() => Guid.Empty;
    public virtual Guid NewGuid()
    {
        if (_guids.TryDequeue(out Guid queueGuid))
        {
            return queueGuid;
        }

        return Guid.NewGuid();
    }

    public virtual void Set(Guid guid)
    {
        _guids.Clear();

        Enqueue(guid);
    }

    public virtual void Enqueue(Guid guid)
    {
        _guids.Enqueue(guid);
    }
}