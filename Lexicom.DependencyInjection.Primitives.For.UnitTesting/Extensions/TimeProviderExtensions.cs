using Lexicom.DependencyInjection.Primitives.For.UnitTesting.Exceptions;

namespace Lexicom.DependencyInjection.Primitives.For.UnitTesting.Extensions;
public static class TimeProviderExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static void Set(this ITimeProvider timeProvider, DateTimeOffset dateTimeOffset)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);

        TestTimeProvider testTimeProvider = GetTestTimeProvider(timeProvider);

        testTimeProvider.Set(dateTimeOffset);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void Enqueue(this ITimeProvider timeProvider, DateTimeOffset dateTimeOffset)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);

        TestTimeProvider testTimeProvider = GetTestTimeProvider(timeProvider);

        testTimeProvider.Enqueue(dateTimeOffset);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void SetLocalTimeZone(this ITimeProvider timeProvider, TimeZoneInfo localTimeZone)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);
        ArgumentNullException.ThrowIfNull(localTimeZone);

        TestTimeProvider testTimeProvider = GetTestTimeProvider(timeProvider);

        testTimeProvider.SetLocalTimeZone(localTimeZone);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void SetTimestampFrequency(this ITimeProvider timeProvider, long timestampFrequency)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);

        TestTimeProvider testTimeProvider = GetTestTimeProvider(timeProvider);

        testTimeProvider.SetTimestampFrequency(timestampFrequency);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void SetTimestamp(this ITimeProvider timeProvider, long timestamp)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);

        TestTimeProvider testTimeProvider = GetTestTimeProvider(timeProvider);

        testTimeProvider.SetTimestamp(timestamp);
    }

    /// <exception cref="ArgumentNullException"/>
    public static void SetElapsedTime(this ITimeProvider timeProvider, TimeSpan elapsedTime)
    {
        ArgumentNullException.ThrowIfNull(timeProvider);

        TestTimeProvider testTimeProvider = GetTestTimeProvider(timeProvider);

        testTimeProvider.SetElapsedTime(elapsedTime);
    }

    private static TestTimeProvider GetTestTimeProvider(ITimeProvider timeProvider)
    {
        if (timeProvider is not TestTimeProvider testTimeProvider)
        {
            throw new NonTestProviderExtensionException<ITimeProvider, TestTimeProvider>();
        }

        return testTimeProvider;
    }
}
