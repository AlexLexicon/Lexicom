using System.Diagnostics;

namespace Lexicom.Extensions.TimeSpans;

public class HowLongAgoTextConfiguration
{
    private static Dictionary<TimeSpanDelineation, (string oneText, string afterTotalText, string? nowText)> DelineationToTexts { get; } = new Dictionary<TimeSpanDelineation, (string oneText, string afterTotalText, string? nowText)>
    {
        { TimeSpanDelineation.Nanoseconds, ("a nanosecond ago"," nanoseconds ago", null) },
        { TimeSpanDelineation.Microseconds, ("a microsecond ago"," microseconds ago", null) },
        { TimeSpanDelineation.Milliseconds, ("a millisecond ago"," milliseconds ago", null) },
        { TimeSpanDelineation.Seconds, ("a second ago"," seconds ago", null) },
        { TimeSpanDelineation.Minutes, ("a minute ago"," minutes ago", "less than a minute ago") },
        { TimeSpanDelineation.Hours, ("an hour ago"," hours ago", "less than an hour ago") },
        { TimeSpanDelineation.Days, ("a day ago"," days ago", "less than a day ago") },
    };

    public HowLongAgoTextConfiguration(TimeSpanDelineation delineation)
    {
        Delineation = delineation;

        if (!DelineationToTexts.TryGetValue(Delineation, out var texts))
        {
            throw new UnreachableException($"The delineation '{delineation}' was not in the '{nameof(DelineationToTexts)}' dictionary.");
        }

        OneText = texts.oneText;
        AfterTotalText = texts.afterTotalText;
        NowText = texts.nowText;
    }
    /// <exception cref="ArgumentNullException"/>
    public HowLongAgoTextConfiguration(
        TimeSpanDelineation delineation, 
        string oneText, 
        string afterTotalText, 
        string? nowText = "a moment ago")
    {
        ArgumentNullException.ThrowIfNull(oneText);
        ArgumentNullException.ThrowIfNull(afterTotalText);

        Delineation = delineation;
        OneText = oneText;
        AfterTotalText = afterTotalText;
        NowText = nowText;
    }

    public TimeSpanDelineation Delineation { get; }
    public string OneText { get; }
    public string AfterTotalText { get; }
    public string? NowText { get; }
}
