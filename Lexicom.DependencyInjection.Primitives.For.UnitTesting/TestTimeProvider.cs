
namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting;
public class TestTimeProvider : ITimeProvider
{
    protected readonly Queue<DateTimeOffset> _dateTimeOffsets;

    public TestTimeProvider()
    {
        _dateTimeOffsets = new Queue<DateTimeOffset>();

        LocalTimeZone = TimeZoneInfo.Local;
        Timer = new TestTimer();
    }

    public TimeZoneInfo LocalTimeZone { get; private set; }
    public long TimestampFrequency { get; private set; }

    private long Timestamp { get; set; }
    private TimeSpan ElapsedTime { get; set; }
    private ITimer Timer { get; set; }

    public DateTimeOffset GetUtcNow() => GetLocalNow();
    public DateTimeOffset GetLocalNow()
    {
        if (_dateTimeOffsets.TryDequeue(out DateTimeOffset queueDateTime))
        {
            return queueDateTime;
        }

        return DateTimeOffset.Now;
    }
    public long GetTimestamp() => Timestamp;
    public TimeSpan GetElapsedTime(long startingTimestamp) => ElapsedTime;
    public TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp) => ElapsedTime;
    public ITimer CreateTimer(TimerCallback callback, object? state, TimeSpan dueTime, TimeSpan period) => Timer;

    public virtual void Set(DateTimeOffset dateTime)
    {
        _dateTimeOffsets.Clear();

        Enqueue(dateTime);
    }
    public virtual void Enqueue(DateTimeOffset dateTime)
    {
        _dateTimeOffsets.Enqueue(dateTime);
    }
    public virtual void SetLocalTimeZone(TimeZoneInfo localTimeZone)
    {
        LocalTimeZone = localTimeZone;
    }
    public virtual void SetTimestampFrequency(long timestampFrequency)
    {
        TimestampFrequency = timestampFrequency;
    }
    public virtual void SetTimestamp(long timestamp)
    {
        Timestamp = timestamp;
    }
    public virtual void SetElapsedTime(TimeSpan elapsedTime)
    {
        ElapsedTime = elapsedTime;
    }
    public virtual void SetTimer(ITimer timer)
    {
        ArgumentNullException.ThrowIfNull(timer);

        Timer = timer;
    }
}
