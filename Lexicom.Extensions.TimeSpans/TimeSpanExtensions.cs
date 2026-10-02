using Lexicom.Extensions.TimeSpans.Exceptions;
using System.Diagnostics;

namespace Lexicom.Extensions.TimeSpans;

public static class TimeSpanExtensions
{
    private static IReadOnlyList<TimeSpanDelineation> OrderedTimeSpanDelineations => field ??= Enum
        .GetValues<TimeSpanDelineation>()
        .Where(d => d is not TimeSpanDelineation.None)
        .OrderByDescending(d => d)
        .ToList();
    private static IReadOnlyList<int> OrderedTimeSpanDelineationIndexes => field ??= Enum
        .GetValues<TimeSpanDelineation>()
        .Cast<int>()
        .Where(i => i is not 0)
        .OrderByDescending(i => i)
        .ToList();

    /// <exception cref="TimeSpanDelineationNoneException"/>
    public static string ToDurationText(this TimeSpan timeSpan, TimeSpanDelineation include = TimeSpanDelineation.Days | TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes)
    {
        string text = string.Empty;

        if (include is TimeSpanDelineation.None)
        {
            throw new TimeSpanDelineationNoneException();
        }

        TimeSpanDelineation? lastDelineation = null;
        foreach (TimeSpanDelineation delineation in OrderedTimeSpanDelineations)
        {
            if (delineation is not TimeSpanDelineation.None && include.HasFlag(delineation))
            {
                if (!string.IsNullOrEmpty(text))
                {
                    text += " ";
                }

                text += GetDelineationText(delineation, allowZeros: false);

                lastDelineation = delineation;
            }
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            if (lastDelineation is null)
            {
                throw new UnreachableException($"There were included delineations provided but the loop did not capture a last used delineation.");
            }

            text = GetDelineationText(lastDelineation.Value, allowZeros: true);
        }

        text = text.Trim();

        return text;

        string GetDelineationText(TimeSpanDelineation delineation, bool allowZeros)
        {
            return delineation switch
            {
                TimeSpanDelineation.None => throw new NotSupportedException($"The delineation '{delineation}' is not supported."),
                TimeSpanDelineation.Nanoseconds => GetTimeText(timeSpan.Nanoseconds, "Nanosecond", allowZeros),
                TimeSpanDelineation.Microseconds => GetTimeText(timeSpan.Microseconds, "Microsecond", allowZeros),
                TimeSpanDelineation.Milliseconds => GetTimeText(timeSpan.Milliseconds, "Millisecond", allowZeros),
                TimeSpanDelineation.Seconds => GetTimeText(timeSpan.Seconds, "Second", allowZeros),
                TimeSpanDelineation.Minutes => GetTimeText(timeSpan.Minutes, "Minute", allowZeros),
                TimeSpanDelineation.Hours => GetTimeText(timeSpan.Hours, "Hour", allowZeros),
                TimeSpanDelineation.Days => GetTimeText(timeSpan.Days, "Day", allowZeros),
                _ => throw new UnreachableException($"The delineation '{delineation}' is not implemented."),
            };
        }

        string GetTimeText(double time, string singularWord, bool allowZero)
        {
            if (!allowZero && time is < 1)
            {
                return string.Empty;
            }

            string text = $"{time} {singularWord}";

            if (time is 1)
            {
                return text;
            }

            return $"{text}s";
        }
    }

    public static string ToHowLongAgoText(this TimeSpan timeSpan) => ToHowLongAgoText(timeSpan, HowLongAgoTextConfigurations.Standard);
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="NoConfigurationsException"/>
    public static string ToHowLongAgoText(this TimeSpan timeSpan, HowLongAgoTextConfigurations configurations)
    {
        ArgumentNullException.ThrowIfNull(configurations);

        if (!configurations.Any())
        {
            throw new NoConfigurationsException();
        }

        int flooredTotal = 0;
        HowLongAgoTextConfiguration? lastConfiguration = null;
        foreach (int index in OrderedTimeSpanDelineationIndexes)
        {
            foreach (HowLongAgoTextConfiguration configuration in configurations)
            {
                int delineationIndex = (int)configuration.Delineation;
                if (index == delineationIndex)
                {
                    double total = index switch
                    {
                        (int)TimeSpanDelineation.Nanoseconds => timeSpan.TotalNanoseconds,
                        (int)TimeSpanDelineation.Microseconds => timeSpan.TotalMicroseconds,
                        (int)TimeSpanDelineation.Milliseconds => timeSpan.TotalMilliseconds,
                        (int)TimeSpanDelineation.Seconds => timeSpan.TotalSeconds,
                        (int)TimeSpanDelineation.Minutes => timeSpan.TotalMinutes,
                        (int)TimeSpanDelineation.Hours => timeSpan.TotalHours,
                        (int)TimeSpanDelineation.Days => timeSpan.TotalDays,
                        _ => throw new UnreachableException($"The delineation index '{index}' is not implemented."),
                    };

                    flooredTotal = (int)Math.Floor(total);

                    lastConfiguration = configuration;

                    if (flooredTotal is > 0)
                    {
                        if (flooredTotal is >= 1 and < 2)
                        {
                            return lastConfiguration.OneText;
                        }

                        return $"{flooredTotal}{configuration.AfterTotalText}";
                    }
                }
            }
        }

        if (lastConfiguration is null)
        {
            throw new UnreachableException($"There were time span configurations provided but the loop did not capture a last used configuration.");
        }

        if (lastConfiguration.NowText is not null)
        {
            return lastConfiguration.NowText;
        }

        return lastConfiguration.OneText;
    }
}
