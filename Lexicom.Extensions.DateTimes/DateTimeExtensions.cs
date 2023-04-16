namespace Lexicom.Extensions.DateTimes;
public static class DateTimeExtensions
{
    private const int SECONDS_IN_MINUTE = 60;
    private const int SECONDS_IN_HOUR = 60 * SECONDS_IN_MINUTE;
    private const int SECONDS_IN_DAY = 24 * SECONDS_IN_HOUR;
    private const int SECONDS_IN_MONTH = 30 * SECONDS_IN_DAY;

    public static string ToRelativeString(this DateTimeOffset dateTimeOffset) => ToRelativeString(dateTimeOffset, new DateTimeRelativeStringSettings());
    /// <exception cref="ArgumentNullException"/>
    public static string ToRelativeString(this DateTimeOffset dateTimeOffset, DateTimeRelativeStringSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        string nowString = settings.NowString ?? "just now";

        var deltaTimeSpan = new TimeSpan(settings.Now.Ticks - dateTimeOffset.Ticks);
        double totalSecondsDelta = Math.Abs(deltaTimeSpan.TotalSeconds);

        if (settings.MinimumDuration >= DateTimeRelativeStringDuration.Seconds && (settings.MaximumDuration <= DateTimeRelativeStringDuration.Seconds || totalSecondsDelta < SECONDS_IN_MINUTE))
        {
            int secondsPast = deltaTimeSpan.Seconds;

            if (secondsPast < 1)
            {
                return nowString;
            }

            if (secondsPast == 1)
            {
                return "one second ago";
            }

            return $"{secondsPast} seconds ago";
        }

        if (settings.MinimumDuration >= DateTimeRelativeStringDuration.Minutes && (settings.MaximumDuration <= DateTimeRelativeStringDuration.Minutes || totalSecondsDelta < 45 * SECONDS_IN_MINUTE))
        {
            int minutesPast = deltaTimeSpan.Minutes;

            if (minutesPast < 1)
            {
                return nowString;
            }

            if (totalSecondsDelta < 2 * SECONDS_IN_MINUTE)
            {
                return "a minute ago";
            }

            return $"{minutesPast} minutes ago";
        }

        if (settings.MinimumDuration >= DateTimeRelativeStringDuration.Hours && (settings.MaximumDuration <= DateTimeRelativeStringDuration.Hours || totalSecondsDelta < 24 * SECONDS_IN_HOUR))
        {
            int hoursPast = deltaTimeSpan.Hours;

            if (hoursPast < 1 || totalSecondsDelta < 90 * SECONDS_IN_MINUTE)
            {
                return "an hour ago";
            }

            return $"{hoursPast} hours ago";
        }

        if (settings.MinimumDuration >= DateTimeRelativeStringDuration.Days && (settings.MaximumDuration <= DateTimeRelativeStringDuration.Days || totalSecondsDelta < 30 * SECONDS_IN_DAY))
        {
            int daysPast = deltaTimeSpan.Days;

            if (daysPast < 1)
            {
                return nowString;
            }

            if (totalSecondsDelta < 48 * SECONDS_IN_HOUR)
            {
                return "yesterday";
            }

            return $"{daysPast} days ago";
        }

        if (settings.MinimumDuration >= DateTimeRelativeStringDuration.Months && (settings.MaximumDuration <= DateTimeRelativeStringDuration.Months || totalSecondsDelta < 12 * SECONDS_IN_MONTH))
        {
            int monthsPast = Convert.ToInt32(Math.Floor((double)deltaTimeSpan.Days / 30));

            if (monthsPast <= 1)
            {
                return "one month ago";
            }

            return $"{monthsPast} months ago";
        }

        if (settings.MinimumDuration >= DateTimeRelativeStringDuration.Years && settings.MaximumDuration <= DateTimeRelativeStringDuration.Years)
        {
            int yearsPast = Convert.ToInt32(Math.Floor((double)deltaTimeSpan.Days / 365));

            if (yearsPast <= 1)
            {
                return "one year ago";
            }

            return $"{yearsPast} years ago";
        }

        return nowString;
    }
}
