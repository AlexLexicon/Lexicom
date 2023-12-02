namespace Lexicom.Extensions.TimeSpans;
public static class TimeSpanExtensions
{
    public static string ToShortestString(this TimeSpan timeSpan) => ToShortestString(timeSpan, new TimeSpanShortestStringSettings());
    /// <exception cref="ArgumentNullException"/>
    public static string ToShortestString(this TimeSpan timeSpan, TimeSpanShortestStringSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        string nowString = settings.NowString ?? "just a moment";

        if (settings.ShortestDurationDescription >= TimeSpanShortestStringDurationDescription.Days || 
            settings.LongestDurationDescription >= TimeSpanShortestStringDurationDescription.Days && 
            timeSpan.Days > 0)
        {
            double totalDays = timeSpan.TotalDays;

            if (totalDays < 1)
            {
                return nowString;
            }

            if (totalDays is >= 1 and < 2)
            {
                return $"a day";
            }

            return $"{totalDays} days";
        }

        if (settings.ShortestDurationDescription >= TimeSpanShortestStringDurationDescription.Hours || 
            settings.LongestDurationDescription >= TimeSpanShortestStringDurationDescription.Hours && 
            timeSpan.Hours > 0)
        {
            double totalHours = timeSpan.TotalHours;

            if (totalHours < 1)
            {
                return nowString;
            }

            if (totalHours is >= 1 and < 2)
            {
                return $"an hour";
            }

            return $"{totalHours} hours";
        }

        if (settings.ShortestDurationDescription >= TimeSpanShortestStringDurationDescription.Minutes || 
            settings.LongestDurationDescription >= TimeSpanShortestStringDurationDescription.Minutes && 
            timeSpan.Minutes > 0)
        {
            double totalMinutes = timeSpan.TotalMinutes;

            if (totalMinutes < 1)
            {
                return nowString;
            }

            if (totalMinutes is >= 1 and < 2)
            {
                return $"a minute";
            }

            return $"{totalMinutes} minutes";
        }

        
        if (settings.ShortestDurationDescription >= TimeSpanShortestStringDurationDescription.Seconds || 
            settings.LongestDurationDescription >= TimeSpanShortestStringDurationDescription.Seconds && 
            timeSpan.Seconds > 0)
        {
            double totalSeconds = timeSpan.TotalSeconds;

            if (totalSeconds < 1)
            {
                return nowString;
            }

            if (totalSeconds is >= 1 and < 2)
            {
                return $"a second";
            }

            return $"{totalSeconds} seconds";
        }


        if (settings.ShortestDurationDescription >= TimeSpanShortestStringDurationDescription.Milliseconds ||
            settings.LongestDurationDescription >= TimeSpanShortestStringDurationDescription.Milliseconds &&
            timeSpan.Milliseconds > 0)
        {
            double totalMilliseconds = timeSpan.TotalMilliseconds;

            if (totalMilliseconds < 1)
            {
                return nowString;
            }

            if (totalMilliseconds is >= 1 and < 2)
            {
                return $"a millisecond";
            }

            return $"{totalMilliseconds} seconds";
        }

        return nowString;
    }
}
