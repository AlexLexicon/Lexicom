namespace Lexicom.DependencyInjection.Primitives;
//baseed on: https://github.com/dotnet/runtime/issues/36617
//replace with dot net 8 implementation when released
public interface ITimeProvider
{
    DateTimeOffset LocalNow { get; }
    DateTimeOffset UtcNow { get; }
    TimeZoneInfo LocalTimeZone { get; }
    long TimestampFrequency { get; }
    long GetTimestamp();
    TimeSpan GetElapsedTime(long startingTimestamp, long endingTimestamp);
}