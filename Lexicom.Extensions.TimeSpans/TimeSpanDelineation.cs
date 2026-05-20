namespace Lexicom.Extensions.TimeSpans;

[Flags]
public enum TimeSpanDelineation
{
    None = 0,
    Nanoseconds = 1,
    Microseconds = 2,
    Milliseconds = 4,
    Seconds = 8,
    Minutes = 16,
    Hours = 32,
    Days = 64,
}
