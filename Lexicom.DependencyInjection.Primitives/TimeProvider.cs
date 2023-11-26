using System.Diagnostics;

namespace Lexicom.DependencyInjection.Primitives;
//based on https://github.com/dotnet/runtime/commit/5d88ff404de77d78e07ad3de7fa2af270332da22
//replace with dot net 8 implementation when released
public abstract class TimeProvider : ITimeProvider
{
    private readonly double _timeToTicksRatio;

    /// <summary>
    /// Gets a <see cref="TimeProvider"/> that provides a clock based on <see cref="DateTimeOffset.UtcNow"/>,
    /// a time zone based on <see cref="TimeZoneInfo.Local"/>, a high-performance time stamp based on <see cref="Stopwatch"/>,
    /// and a timer based on <see cref="Timer"/>.
    /// </summary>
    /// <remarks>
    /// If the <see cref="TimeZoneInfo.Local"/> changes after the object is returned, the change will be reflected in any subsequent operations that retrieve <see cref="TimeProvider.LocalNow"/>.
    /// </remarks>
    public static TimeProvider System { get; } = new SystemTimeProvider(null);

    /// <summary>
    /// Initializes the instance with the timestamp frequency.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">The value of <paramref name="timestampFrequency"/> is negative or zero.</exception>
    /// <param name="timestampFrequency">Frequency of the values returned from <see cref="GetTimestamp"/> method.</param>
    protected TimeProvider(long timestampFrequency)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(timestampFrequency);

        TimestampFrequency = timestampFrequency;
        _timeToTicksRatio = (double)TimeSpan.TicksPerSecond / TimestampFrequency;
    }

    /// <summary>
    /// Gets a <see cref="DateTimeOffset"/> value whose date and time are set to the current
    /// Coordinated Universal Time (UTC) date and time and whose offset is Zero,
    /// all according to this <see cref="TimeProvider"/>'s notion of time.
    /// </summary>
    public abstract DateTimeOffset UtcNow { get; }

    /// <summary>
    /// Gets a <see cref="DateTimeOffset"/> value that is set to the current date and time according to this <see cref="TimeProvider"/>'s
    /// notion of time based on <see cref="UtcNow"/>, with the offset set to the <see cref="LocalTimeZone"/>'s offset from Coordinated Universal Time (UTC).
    /// </summary>
    public DateTimeOffset LocalNow
    {
        get
        {
            DateTime utcDateTime = UtcNow.UtcDateTime;
            TimeSpan offset = LocalTimeZone.GetUtcOffset(utcDateTime);

            long localTicks = utcDateTime.Ticks + offset.Ticks;
            long minTicks = 0L;
            long maxTicks = 3155378975999999999L;
            if (localTicks > maxTicks)
            {
                localTicks = localTicks < minTicks ? minTicks : maxTicks;
            }

            return new DateTimeOffset(localTicks, offset);
        }
    }

    /// <summary>
    /// Gets a <see cref="TimeZoneInfo"/> object that represents the local time zone according to this <see cref="TimeProvider"/>'s notion of time.
    /// </summary>
    public abstract TimeZoneInfo LocalTimeZone { get; }

    /// <summary>
    /// Gets the frequency of <see cref="GetTimestamp"/> of high-frequency value per second.
    /// </summary>
    public long TimestampFrequency { get; }

    /// <summary>
    /// Creates a <see cref="TimeProvider"/> that provides a clock based on <see cref="DateTimeOffset.UtcNow"/>,
    /// a time zone based on <paramref name="timeZone"/>, a high-performance time stamp based on <see cref="Stopwatch"/>,
    /// and a timer based on <see cref="Timer"/>.
    /// </summary>
    /// <param name="timeZone">The time zone to use in getting the local time using <see cref="LocalNow"/>. </param>
    /// <returns>A new instance of <see cref="TimeProvider"/>. </returns>
    /// <exception cref="ArgumentNullException"><paramref name="timeZone"/> is null.</exception>
    public static TimeProvider FromLocalTimeZone(TimeZoneInfo timeZone)
    {
        ArgumentNullException.ThrowIfNull(timeZone);
        return new SystemTimeProvider(timeZone);
    }

    /// <summary>
    /// Gets the current high-frequency value designed to measure small time intervals with high accuracy in the timer mechanism.
    /// </summary>
    /// <returns>A long integer representing the high-frequency counter value of the underlying timer mechanism. </returns>
    public abstract long GetTimestamp();

    /// <summary>
    /// Gets the elapsed time between two timestamps retrieved using <see cref="GetTimestamp"/>.
    /// </summary>
    /// <param name="startingTimestamp">The timestamp marking the beginning of the time period.</param>
    /// <param name="endingTimestamp">The timestamp marking the end of the time period.</param>
    /// <returns>A <see cref="TimeSpan"/> for the elapsed time between the starting and ending timestamps.</returns>
    public TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp) => new TimeSpan((long)((endingTimestamp - startingTimestamp) * _timeToTicksRatio));

    /// <summary>
    /// Provides a default implementation of <see cref="TimeProvider"/> based on <see cref="DateTimeOffset.UtcNow"/>,
    /// <see cref="TimeZoneInfo.Local"/>, <see cref="Stopwatch"/>, and <see cref="Timer"/>.
    /// </summary>
    private sealed class SystemTimeProvider : TimeProvider
    {
        /// <summary>The time zone to treat as local.  If null, <see cref="TimeZoneInfo.Local"/> is used.</summary>
        private readonly TimeZoneInfo? _localTimeZone;

        /// <summary>Initializes the instance.</summary>
        /// <param name="localTimeZone">The time zone to treat as local.  If null, <see cref="TimeZoneInfo.Local"/> is used.</param>
        internal SystemTimeProvider(TimeZoneInfo? localTimeZone) : base(Stopwatch.Frequency) => _localTimeZone = localTimeZone;

        /// <inheritdoc/>
        public override TimeZoneInfo LocalTimeZone => _localTimeZone ?? TimeZoneInfo.Local;

        /// <inheritdoc/>
        public override long GetTimestamp() => Stopwatch.GetTimestamp();

        /// <inheritdoc/>
        public override DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}