namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting;
public class TestTimer : ITimer
{
    private bool ChangeValue { get; set; }

    public bool Change(TimeSpan dueTime, TimeSpan period) => ChangeValue;
    public void Dispose() { }
    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    public virtual void SetChange(bool change)
    {
        ChangeValue = change;
    }
}
