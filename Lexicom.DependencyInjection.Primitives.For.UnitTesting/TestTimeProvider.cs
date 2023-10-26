namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting;
public abstract class TestTimeProvider : ITimeProvider
{
    protected readonly Queue<DateTimeOffset> _dateTimeOffsets;

    public TestTimeProvider()
    {
        _dateTimeOffsets = new Queue<DateTimeOffset>();

        LocalTimeZone = TimeZoneInfo.Local;
    }

    public DateTimeOffset LocalNow
    {
        get
        {
            if (_dateTimeOffsets.TryDequeue(out DateTimeOffset queueDateTime))
            {
                return queueDateTime;
            }

            return DateTimeOffset.Now;
        }
    }
    public DateTimeOffset UtcNow => LocalNow;
    public TimeZoneInfo LocalTimeZone { get; private set; }
    public long TimestampFrequency { get; private set; }

    private long Timestamp { get; set; }
    private TimeSpan ElapsedTime { get; set; }

    public long GetTimestamp() => Timestamp;
    public TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp) => ElapsedTime;

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
}
